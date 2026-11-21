using Microsoft.AspNetCore.Identity;
using Backend.DTOs;
using System.Security.Claims;
using Backend.Data;

namespace Backend.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly AppDbContext _context;

    public AuthService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        AppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    public async Task<IdentityResult> Register(RegisterDto dto)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        var user = new AppUser
        {
            UserName = dto.UserName,
            Email = dto.Email
        };

        var createResult = await _userManager.CreateAsync(user, dto.Password);

        if (!createResult.Succeeded)
            return createResult;

        var roleResult = await _userManager.AddToRoleAsync(user, Roles.Student);

        if (!roleResult.Succeeded)
            return roleResult;

        await transaction.CommitAsync();

        return IdentityResult.Success;
    }

    public async Task<SignInResult> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user == null)
            return SignInResult.Failed;

        return await _signInManager.PasswordSignInAsync(
            user.UserName!,
            dto.Password,
            isPersistent: false,
            lockoutOnFailure: true
        );
    }


    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }


    public async Task<UserDto?> GetCurrentUser(ClaimsPrincipal User)
    {
        // wyciąganie ciasteczka po otwarciu strony (lub przeładowaniu)
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
            return null;

        var roles = await _userManager.GetRolesAsync(user);

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            UserName = user.UserName!,
            Roles = roles.ToList()
        };
    }

    
}