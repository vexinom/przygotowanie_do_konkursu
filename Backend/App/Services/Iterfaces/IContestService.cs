using Backend.DTOs;

namespace Backend.Services;

public interface IContestService
{
    Task<IEnumerable<ContestDto>> GetAll();

    Task<ContestDto?> GetById(Guid id);

    Task<ContestDto> Create(CreateContestDto dto);

    Task<ContestDto> Update(Guid id, UpdateContestDto dto);

    Task Delete(Guid id);
}