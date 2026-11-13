using Backend.Models;
using Backend.DTOs;

namespace Backend.Services;

public class ProblemService : IProblemService
{
    public Task<IEnumerable<ProblemDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<ProblemDto> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ProblemDto> Create(CreateProblemDto dto, Guid creatorId)
    {
        throw new NotImplementedException();
    }

    public Task<ProblemDto> Update(Guid id, UpdateProblemDto dto)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}