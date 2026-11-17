using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.DTOs;
using Backend.Models;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoryController : ControllerBase
{
    private readonly IHistoryService _service;

    public HistoryController(IHistoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<HistoryDto>>> GetAll()
        => Ok(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<ActionResult<HistoryDto>> GetById(Guid id)
    {
        var contest = await _service.GetById(id);

        if (contest is null)
            return NotFound();

        return Ok(contest);
    }
    [HttpGet("{id}-{from_date}-{to_date}")]
    public async Task<ActionResult<HistoryDto>> GetById(Guid id, DateTime from_date, DateTime to_date)
    {
        var contest = await _service.GetAll();

        if (contest is null)
            return NotFound();
        contest.Where(c => c.LastInteractionTime > from_date && c.LastInteractionTime < to_date)
            .ToList();

        return Ok(contest);
    }


    [HttpPost]
    public async Task<ActionResult<HistoryDto>> Create(CreateHistoryDto dto)
        => Ok(await _service.Create(dto));

    [HttpPut("{id}")]
    public async Task<ActionResult<HistoryDto>> Update(Guid id, UpdateHistoryDto dto)
        => Ok(await _service.Update(id, dto));

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}
