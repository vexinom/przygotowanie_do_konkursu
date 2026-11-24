using Microsoft.AspNetCore.Mvc;
using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Teacher")]
public class ProblemController : ControllerBase
{
    private readonly IProblemService _service;

    public ProblemController(IProblemService service)
    {
        _service = service;
    }


    [HttpGet] 
    public async Task<ActionResult<List<ProblemDto>>> GetAll() 
        => Ok(await _service.GetAll());
            
    [HttpGet("{id}")]
    public async Task<ActionResult<ProblemDto>> GetById(Guid id)
    {
        var problem = await _service.GetById(id);

        if (problem is null)
            return NotFound();

        return Ok(problem);
    }

    [HttpPost]
    public async Task<ActionResult<ProblemDto>> Create(CreateProblemDto dto)
        => Ok(await _service.Create(dto));

    [HttpPut("{id}")]
    public async Task<ActionResult<ProblemDto>> Update(Guid id, UpdateProblemDto dto)
        => Ok(await _service.Update(id, dto));

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}