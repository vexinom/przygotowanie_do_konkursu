using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Backend.Data;

namespace Backend.Services;

public class ContestService : IContestService
{
    private readonly AppDbContext _context;

    public ContestService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ContestDto>> GetAll()
    {
        return await _context.Contests
            .Where(c => !c.IsDeleted)
            .Select(c => new ContestDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            })
            .ToListAsync();
        } 

    public async Task<ContestDto?> GetById(Guid id)
    {
        return await _context.Contests
            .Where(c => !c.IsDeleted && c.Id == id)
            .Select(c => new ContestDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            })
            .FirstOrDefaultAsync();
    } 

    public async Task<ContestDto> Create(CreateContestDto dto)
    {
        var contest = new Contest
        {
            Name = dto.Name,
            Description = dto.Description!
        };

        _context.Add(contest);
        await _context.SaveChangesAsync();

        return new ContestDto
        {
            Id = contest.Id,
            Name = dto.Name,
            Description = dto.Description
        };
    } 

    public async Task<ContestDto> Update(Guid id, UpdateContestDto dto)
    {
        var contest = await _context.Contests
            .FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id)
            ?? throw new KeyNotFoundException("Problem not found.");
        
        contest.Name = dto.Name;
        contest.Description = dto.Description;

        await _context.SaveChangesAsync();
        
        return new ContestDto
        {
            Id = contest.Id,
            Name = contest.Name,
            Description = contest.Description  
        };
    } 

    public async Task Delete(Guid id)
    {
        var problem = await _context.Contests
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted)
            ?? throw new KeyNotFoundException("Problem not found.");
        
        problem.IsDeleted = true;
        await _context.SaveChangesAsync();
    }
}