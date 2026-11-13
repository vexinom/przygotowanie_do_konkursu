using Microsoft.AspNetCore.Mvc;
using Backend.DTOs;
using Backend.Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProblemController : ControllerBase
{

    private readonly IProblemService _service;

    public ProblemController(IProblemService service)
    {
        _service = service;
    }


    [HttpGet] //pobranie listy zadań
    public async Task<ActionResult<ProblemDto>> GetAll() //task, bo zwrot asynchroniczny!
        => Ok(await _service.GetAll());

    [HttpGet("{id}")] //pobranie jednego zadania
    public async Task<ActionResult<ProblemDto>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult> Create()
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update()
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete()
    {
        throw new NotImplementedException();
    }


}