using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserProgressController : ControllerBase
{
    private readonly IUserProgressService _progressService;

    public UserProgressController(IUserProgressService progressService)
    {
        _progressService = progressService;
    }

    [HttpGet]
    public async Task<ActionResult<List<HistoryEntryDto>>> GetUserHistory(
        [FromQuery] Guid? userId,
        [FromQuery] HistoryFilterDto filter)
    {
        var history = await _progressService.GetUserHistory(User, userId, filter);
        return Ok(history);
    }
}



    // [HttpGet]
    // public async Task<ActionResult<List<ProblemProgressDto>>> GetAll()
    //     => Ok(await _service.GetAll());

    // [HttpGet("{id}")]
    // public async Task<ActionResult<ProblemProgressDto>> GetById(Guid id)
    // {
    //     var contest = await _service.GetById(id);

    //     if (contest is null)
    //         return NotFound();

    //     return Ok(contest);
    // }
    // [HttpGet("{from_date}/{to_date}")]
    // public async Task<ActionResult<ProblemProgressDto>> FilterList(DateTime from_date, DateTime to_date, [FromQuery] List<SolveState>? states)
    // {
    //     var contest = await _service.GetAll();

    //     if (contest is null)
    //         return NotFound();

    //     if (states != null && states.Any())
    //     {
    //         return Ok(contest.Where(c => c.LastInteractionTime > from_date && c.LastInteractionTime < to_date && states.Contains(c.State))
    //             .ToList());

    //     }

    //     return Ok(contest.Where(c => c.LastInteractionTime > from_date && c.LastInteractionTime < to_date)
    //         .ToList());
    // }


    // [HttpPost]
    // public async Task<ActionResult<ProblemProgressDto>> Create(SubmitProblemAttemptDto dto)
    //     => Ok(await _service.Create(dto));

    // [HttpPut("{id}")]
    // public async Task<ActionResult<ProblemProgressDto>> Update(Guid id, UpdateProblemAttemptDto dto)
    //     => Ok(await _service.Update(id, dto));

    // [HttpDelete("{id}")]
    // public async Task<ActionResult> Delete(Guid id)
    // {
    //     await _service.Delete(id);
    //     return NoContent();
    // }
