using Backend.Models;
using Backend.DTOs;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Backend.Data;

namespace Backend.Services;

public class ProblemService : IProblemService
{
    private readonly AppDbContext _context;

    public ProblemService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProblemSummaryDto>> GetAll()
    {
        return await _context.Problems
            .Where(p => !p.IsDeleted)
            .OrderBy(p => p.Content)
            .Select(p => new ProblemSummaryDto
            {
                Id = p.Id,
                Content = p.Content,
                AnswersCount = p.Answers.Count(a => !a.IsDeleted),
                ContestId = p.ContestId
            })
            .ToListAsync();
    }

    public async Task<ProblemDto?> GetById(Guid id)
    {
        return await _context.Problems
            .Where(p => p.Id == id && !p.IsDeleted )
            .Select(p => new ProblemDto
            {
                Id = p.Id,
                Content = p.Content,
                Answers = p.Answers
                    .Where(a => !a.IsDeleted)
                    .Select(a => new AnswerDto
                    {
                        Id = a.Id,
                        Content = a.Content,
                        IsCorrect = a.IsCorrect
                    })
                    .ToList(),
                ContestId = p.ContestId
            })
            .FirstOrDefaultAsync();
            
    }

    public async Task<ProblemDto> Create(CreateProblemDto dto)
    {
        //sprawdzenie ilości dodanych odpowiedzi jest w Dto (a przyjanmniej powinno...)
        //zakładam, że przynajmniej jedna musi być poprawna
        if (dto.Answers.Any(a => a.IsCorrect) == false)
        {
            throw new ValidationException("At least one answer must be correct.");
        }
    

        var problem = new Problem
        {
            Content = dto.Content,
            IsDeleted = false,
            ContestId = dto.ContestId,
            Answers = dto.Answers.Select(a => new Answer
            {
                Content = a.Content,
                IsCorrect = a.IsCorrect,
                IsDeleted = false
            }).ToList()
        };

        _context.Problems.Add(problem);
        await _context.SaveChangesAsync();

        return new ProblemDto
        {
            Id = problem.Id,
            Content = problem.Content,
            ContestId = problem.ContestId,
            Answers = problem.Answers
                .Where(a => !a.IsDeleted)
                .Select(a => new AnswerDto
                {
                    Id = a.Id,
                    Content = a.Content,
                    IsCorrect = a.IsCorrect
                })
                .ToList()
        };
    }

    public async Task<ProblemDto> Update(Guid id, UpdateProblemDto dto)
    {
        var problem = await _context.Problems
            .Include(p => p.Answers)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        if (problem is null)
        {
            throw new KeyNotFoundException("Problem not found.");
        }

        if (dto.Answers.Any(a => a.IsCorrect) == false)
        {
            throw new ValidationException("At least one answer must be correct.");
        }

        problem.Content = dto.Content; //aktalizacja polecenia
        problem.ContestId = dto.ContestId;

        var existingAnswers = problem.Answers
            .Where(a => !a.IsDeleted)
            .ToList();
        foreach (var answerDto in dto.Answers) //aktualizacja/dodanie odpowiedzi
        {
            // Istniejąca odpowiedź
            if (answerDto.Id.HasValue)
            {
                var answer = problem.Answers
                    .FirstOrDefault(a => a.Id == answerDto.Id.Value);

                if (answer == null || answer.IsDeleted)
                    throw new KeyNotFoundException("Answer not found.");

                answer.Content = answerDto.Content;
                answer.IsCorrect = answerDto.IsCorrect;
            }
            // Nowa odpowiedź
            else
            {
                var newAnswer = new Answer
                {
                    Content = answerDto.Content,
                    IsCorrect = answerDto.IsCorrect,
                    IsDeleted = false,
                    Problem = problem
                };

                _context.Answers.Add(newAnswer);
            }
        }

        var submittedIds = dto.Answers //usuwanie odpowiedzi (wybiera id przesłanych odp)
            .Where(a => a.Id.HasValue)
            .Select(a => a.Id!.Value)
            .ToHashSet();

        foreach (var answer in existingAnswers)
        {
            if (!submittedIds.Contains(answer.Id))
            {
                answer.IsDeleted = true;
            }
        }


        await _context.SaveChangesAsync();


        return new ProblemDto
        {
            Id = problem.Id,
            Content = problem.Content,
            ContestId = problem.ContestId,
            Answers = problem.Answers
                .Where(a => !a.IsDeleted)
                .Select(a => new AnswerDto
                {
                    Id = a.Id,
                    Content = a.Content,
                    IsCorrect = a.IsCorrect
                })
                .ToList()
        };
    }

    public async Task Delete(Guid id)
    {
        var problem = await _context.Problems
            .Include(p => p.Answers)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        if (problem == null)
        {
            throw new KeyNotFoundException("Problem not found.");
        }

        problem.IsDeleted = true;
        
        foreach (var answer in problem.Answers)
        {
            answer.IsDeleted = true;
        }

        await _context.SaveChangesAsync();
    }
}