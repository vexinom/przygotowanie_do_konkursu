using Backend.DTOs;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Backend.Data;
using AutoMapper;
using Backend.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Backend.Exceptions;

namespace Backend.Services;

public class UserProgressService : IUserProgressService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public UserProgressService(AppDbContext context, IMapper mapper, UserManager<AppUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<List<HistoryEntryDto>> GetUserHistory(
        ClaimsPrincipal principal, Guid? requestedUserId, HistoryFilterDto filter)
    {
        var currentUserId = Guid.Parse(_userManager.GetUserId(principal)
            ?? throw new UnauthorizedAccessException());

        var targetUserId = requestedUserId ?? currentUserId;

        if (targetUserId != currentUserId &&
            !principal.IsInRole(Roles.Admin) && !principal.IsInRole(Roles.Teacher))
            throw new ForbiddenAccessException();


        var query = _context.History
            .Where(e => e.UserId == targetUserId);

        if (filter.FromDate.HasValue)
            query = query.Where(e => e.LastAttemptAt >= filter.FromDate.Value);

        if (filter.ToDate.HasValue)
            query = query.Where(e => e.LastAttemptAt <= filter.ToDate.Value);

        if (filter.IsCorrect.HasValue)
            query = query.Where(e => e.IsCorrect == filter.IsCorrect.Value);

        query.OrderByDescending(e => e.LastAttemptAt);

        return await query
            .ProjectTo<HistoryEntryDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }


    public async Task RecordAttempt(Guid userId, Guid problemId, bool isCorrect)
    {
        var entry = await _context.History
            .Where(e => e.UserId == userId && e.ProblemId == problemId)
            .FirstOrDefaultAsync();

        var now = DateTime.UtcNow;

        if (entry is null)
        {
            _context.Add(new HistoryEntry
            {
                IsCorrect = isCorrect,
                UserId = userId,
                ProblemId = problemId,
                FirstSolvedAt = isCorrect ? now : null
            });
            await _context.SaveChangesAsync();
            return;
        }

        entry.TotalAttempts++;
        entry.LastAttemptAt = now;

        if (!entry.IsCorrect) 
        {
            entry.AttemptsToSolve++;

            if (isCorrect) //pierwsze poprawne rozwiązanie
            {
                entry.FirstSolvedAt = now;
                entry.IsCorrect = true;
            }
        }

        await _context.SaveChangesAsync();
    }


    // tu potem można dodawać więcej statystyk np. zliczenie procentowe wykonania wszystkich zadań itp.


   

























    // public async Task<ProblemProgressDto> Create(SubmitProblemAttemptDto dto)
    // {
    //     var user = await 

    //     var entry = new HistoryEntry
    //     {
    //         State = dto.State,
    //         LastInteractionTime = dto.LastInteractionTime,
    //         ProblemId = dto.ProblemId,
    //     };

    //     _context.Add(entry);
    //     await _context.SaveChangesAsync();

    //     return new ProblemProgressDto
    //     {
    //         UserId = 
    //         Id = entry.Id,
    //         State = dto.State,
    //         LastInteractionTime = dto.LastInteractionTime,
    //         ProblemId = dto.ProblemId,
    //     };
    // }

    // public async Task<ProblemProgressDto> Update(Guid id, UpdateProblemAttemptDto dto)
    // {
    //     var entry = await _context.History
    //         .FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id)
    //         ?? throw new KeyNotFoundException("Entry not found.");

    //     entry.State = dto.State;
    //     entry.LastInteractionTime = dto.LastInteractionTime;

    //     await _context.SaveChangesAsync();

    //     return new ProblemProgressDto
    //     {
    //         UserId = entry.UserId,
    //         Id = entry.Id,
    //         State = dto.State,
    //         LastInteractionTime = dto.LastInteractionTime,
    //         ProblemId = entry.ProblemId
    //     };
    // }

    // public async Task Delete(Guid id)
    // {
    //     var entry = await _context.History
    //         .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted)
    //         ?? throw new KeyNotFoundException("Entry not found.");

    //     entry.IsDeleted = true;
    //     await _context.SaveChangesAsync();
    // }

    // private async Task<List<ProblemProgressDto>> FetchHistoryByUserIdAsync(Guid userId)
    // {
    //     return await _context.History
    //         .AsNoTracking() //pobranie bez śledzenia zmian
    //         .Where(h => h.UserId == userId)
    //         .OrderByDescending(h => h.LastInteractionTime)
    //         .Select(entry => new ProblemProgressDto
    //         {
    //             UserId = entry.UserId,
    //             Id = entry.Id,
    //             State = entry.State,
    //             LastInteractionTime = entry.LastInteractionTime,
    //             ProblemId = entry.ProblemId   
    //         })
    //         .ToListAsync();
    // }

    
}
