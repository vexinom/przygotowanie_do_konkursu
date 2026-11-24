using System.Security.Claims;
using Backend.DTOs;

namespace Backend.Services;

public interface IUserProgressService
{
    Task<List<HistoryEntryDto>> GetUserHistory(ClaimsPrincipal principal, Guid? requestedUserId, HistoryFilterDto filter);
    public Task RecordAttempt(Guid userId, Guid problemId, bool isCorrect);
    
}