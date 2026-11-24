using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/solving")]
[Authorize]
public class SolvingController : ControllerBase
{
    private readonly ISolvingService _solvingService;

    public SolvingController(ISolvingService solvingService)
        => _solvingService = solvingService;


    [HttpGet("next")]
    public async Task<ActionResult<ProblemToSolveDto>> Next()
    {
        var problem = await _solvingService.GetNextProblem(User);
        return problem is null ? NoContent() : Ok(problem);
    }

    [HttpPost("answer")]
    public async Task<ActionResult<AnswerResultDto>> Answer(SubmitAnswerDto dto)
        => Ok(await _solvingService.SubmitAnswer(User, dto));
}