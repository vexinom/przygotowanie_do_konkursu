using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.DTOs;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContestController : ControllerBase
{
    private readonly IContestService _service;
    
    public ContestController(IContestService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ContestDto>>> GetAll()
        => Ok(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<ActionResult<ContestDto>> GetById(Guid id)
        => Ok(await _service.GetById(id));

    [HttpPost]
    public async Task<ActionResult<ContestDto>> Create(CreateContestDto dto)
        => Ok(await _service.Create(dto));

    [HttpPut("{id}")]
    public async Task<ActionResult<ContestDto>> Update(Guid id, UpdateContestDto dto)
        => Ok(await _service.Update(id, dto));

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}