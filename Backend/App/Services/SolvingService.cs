using System.Security.Claims;
using Backend.Data;
using Backend.DTOs;
using Backend.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class SolvingService : ISolvingService
{
    private readonly AppDbContext _context;
    private readonly IProblemSelector _selector;
    private readonly IUserProgressService _progressService;

    public SolvingService(
        AppDbContext context,
        IProblemSelector selector,
        IUserProgressService progressService)
    {
        _context = context;
        _selector = selector;
        _progressService = progressService;
    }

    public async Task<ProblemToSolveDto?> GetNextProblem(ClaimsPrincipal principal)
    {
        var userId = principal.GetUserId();

        var problemId = await _selector.SelectNextProblemId(userId);
        if (problemId is null)
            return null;

        return await _context.Problems
            .Where(p => p.Id == problemId && !p.IsDeleted)
            .Select(p => new ProblemToSolveDto
            {
                Id = p.Id,
                Content = p.Content,
                Answers = p.Answers
                    .Where(a => !a.IsDeleted)
                    .Select(a => new AnswerOptionDto { Id = a.Id, Content = a.Content })
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<AnswerResultDto> SubmitAnswer(ClaimsPrincipal principal, SubmitAnswerDto dto)
    {
        var userId = principal.GetUserId();

        var correctIds = await _context.Answers
            .Where(a => a.ProblemId == dto.ProblemId
                        && !a.IsDeleted
                        && a.IsCorrect
                        && !a.Problem.IsDeleted)
            .Select(a => a.Id)
            .ToListAsync();

        // Create/Update wymuszają min. jedną poprawną odpowiedź,
        // więc pusta lista oznacza brak (lub usunięcie) zadania.
        if (correctIds.Count == 0)
            throw new KeyNotFoundException("Problem not found.");

        // poprawne tylko, gdy zaznaczony zbiór == zbiór poprawnych odpowiedzi
        var isCorrect = correctIds.ToHashSet().SetEquals(dto.SelectedAnswerIds);

        await _progressService.RecordAttempt(userId, dto.ProblemId, isCorrect);

        return new AnswerResultDto
        {
            IsCorrect = isCorrect,
            CorrectAnswerIds = correctIds
        };
    }
}