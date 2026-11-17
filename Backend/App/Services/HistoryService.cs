using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Backend.Data;

namespace Backend.Services;

public class HistoryService : IHistoryService
{
    private readonly AppDbContext _context;

    public HistoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<HistoryDto>> GetAll()
    {
        return await _context.History
            .Where(c => !c.IsDeleted)
            .Select(c => new HistoryDto
            {
                Id = c.Id,
                State = c.State,
                LastInteractionTime = c.LastInteractionTime,
                ProblemId = c.ProblemId,
            })
            .ToListAsync();
        }

    public async Task<HistoryDto?> GetById(Guid id)
    {
        return await _context.History
            .Where(c => !c.IsDeleted && c.Id == id)
            .Select(c => new HistoryDto
            {
                Id = c.Id,
                State = c.State,
                LastInteractionTime = c.LastInteractionTime,
                ProblemId = c.ProblemId,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<HistoryDto> Create(CreateHistoryDto dto)
    {
        var contest = new HistoryEntry
        {
            State = dto.State,
            LastInteractionTime = dto.LastInteractionTime,
            ProblemId = dto.ProblemId,
        };

        _context.Add(contest);
        await _context.SaveChangesAsync();

        return new HistoryDto
        {
            Id = contest.Id,
            State = dto.State,
            LastInteractionTime = dto.LastInteractionTime,
            ProblemId = dto.ProblemId,
        };
    }

    public async Task<HistoryDto> Update(Guid id, UpdateHistoryDto dto)
    {
        var contest = await _context.History
            .FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id)
            ?? throw new KeyNotFoundException("Problem not found.");

        contest.State = dto.State;
        contest.LastInteractionTime = dto.LastInteractionTime;

        await _context.SaveChangesAsync();

        return new HistoryDto
        {
            Id = contest.Id,
            State = dto.State,
            LastInteractionTime = dto.LastInteractionTime,
            ProblemId = contest.ProblemId,
        };
    }

    public async Task Delete(Guid id)
    {
        var entry = await _context.History
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted)
            ?? throw new KeyNotFoundException("Problem not found.");

        entry.IsDeleted = true;
        await _context.SaveChangesAsync();
    }
}
