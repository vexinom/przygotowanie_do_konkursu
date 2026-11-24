using Backend.DTOs;

namespace Backend.Services;

public interface IProblemService
{
    Task<IEnumerable<ProblemSummaryDto>> GetAll();

    Task<ProblemDto?> GetById(Guid id);

    Task<ProblemDto> Create(CreateProblemDto dto);

    Task<ProblemDto> Update(Guid id, UpdateProblemDto dto);

    Task Delete(Guid id);
}