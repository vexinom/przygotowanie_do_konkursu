using System.Security.Claims;
using Backend.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Backend.Services;

public interface IAuthService
{
    Task<IdentityResult> Register(RegisterDto dto);

    Task<SignInResult> Login(LoginDto dto);

    Task Logout();

    Task<UserDto?> GetCurrentUser(ClaimsPrincipal User);
}