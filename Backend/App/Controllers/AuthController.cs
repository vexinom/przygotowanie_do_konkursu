using Microsoft.AspNetCore.Mvc;
using Backend.DTOs;
using Microsoft.AspNetCore.Authorization;
using Backend.Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;
    
    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var result = await _service.Register(dto);
        if (result.Succeeded) return Ok();
        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await _service.Login(dto);

        if (result.Succeeded) 
            return Ok();

        if (result.IsLockedOut)
            return StatusCode(StatusCodes.Status423Locked, new 
            { 
                message = "Zbyt wiele nieudanych prób. Spróbuj ponownie za 5 min." 
            });

        if (result.IsNotAllowed)
        return BadRequest(new
        {
            message = "Nie można zalogować użytkownika."
        });

        return Unauthorized(new
        {
            message = "Nieprawidłowy email lub hasło."
        });
    }
    

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _service.Logout();
        return Ok();
    }

    [HttpGet("me")]
    [Authorize] 
    public async Task<IActionResult> GetCurrentUser()
    {
        var result = await _service.GetCurrentUser(User);
        if (result is null) return Unauthorized();

        return Ok(result);
    }
}