using Backend.DTOs;

namespace Backend.Services;

public interface IHistoryService
{
    Task<IEnumerable<HistoryDto>> GetAll();

    Task<HistoryDto?> GetById(Guid id);

    Task<HistoryDto> Create(CreateHistoryDto dto);

    Task<HistoryDto> Update(Guid id, UpdateHistoryDto dto);

    Task Delete(Guid id);
}
