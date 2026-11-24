using System.Security.Claims;
using Backend.DTOs;

namespace Backend.Services;

public interface ISolvingService
{
    Task<ProblemToSolveDto?> GetNextProblem(ClaimsPrincipal principal);
    Task<AnswerResultDto> SubmitAnswer(ClaimsPrincipal principal, SubmitAnswerDto dto);
}