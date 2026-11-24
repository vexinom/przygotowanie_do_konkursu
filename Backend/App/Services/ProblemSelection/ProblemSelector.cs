using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class RandomProblemSelector : IProblemSelector
{
    private readonly AppDbContext _context;

    public RandomProblemSelector(AppDbContext context) => _context = context;

    public async Task<Guid?> SelectNextProblemId(Guid userId)
    {
        return await _context.Problems
            .Where(p => !p.IsDeleted)
            .OrderBy(p => EF.Functions.Random())
            .Select(p => (Guid?)p.Id)
            .FirstOrDefaultAsync();
    }
}

//można dodać więcej różnych z lepszą logiką niż losowo........