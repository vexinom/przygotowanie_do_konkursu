using Microsoft.AspNetCore.Identity;
using Backend.DTOs;
using System.Security.Claims;
using Backend.Data;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Org.BouncyCastle.Crypto;

namespace Backend.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly AppDbContext _context;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        AppDbContext context,
        IEmailService emailService,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _emailService = emailService;
        _configuration = configuration;
    }

    public async Task<IdentityResult> Register(RegisterDto dto)
    {
        

        var user = new AppUser
        {
            UserName = dto.UserName,
            Email = dto.Email
        };

        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        var createResult = await _userManager.CreateAsync(user, dto.Password);

        if (!createResult.Succeeded)
            return createResult;

        var roleResult = await _userManager.AddToRoleAsync(user, Roles.Student);

        if (!roleResult.Succeeded)
            return roleResult;

        await transaction.CommitAsync();
        
        await SendConfirmationEmail(user);
        
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


    public async Task<UserDto?> GetCurrentUser(ClaimsPrincipal principal)
    {
        // wyciąganie ciasteczka po otwarciu strony (lub przeładowaniu)
        var user = await _userManager.GetUserAsync(principal);
        if (user is null)
            return null;

        var roles = await _userManager.GetRolesAsync(user);

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            UserName = user.UserName!,
            Roles = roles.ToList(),
            EmalConfirmed = user.EmailConfirmed
        };
    }

    public async Task<IdentityResult> ChangePassword(ClaimsPrincipal principal, ChangePasswordDto dto)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user is null)
            return IdentityResult.Failed();


        var result = await _userManager.ChangePasswordAsync(
            user,
            dto.CurrentPassword,
            dto.NewPassword
        );

        return result;
    }

    public async Task ForgotPassword(ForgotPasswordDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null)
            throw new UnauthorizedAccessException();

        if (!await _userManager.IsEmailConfirmedAsync(user))
            return;

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        if (string.IsNullOrEmpty(token))
            throw new InvalidOperationException("coś poszło nie tak");

        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token)); //pozbywa się znaków specjalnych z tokenu

        var frontendUrl = _configuration["Frontend:BaseUrl"];

            var resetLink = QueryHelpers.AddQueryString(
                $"{frontendUrl}/reset-password",
                new Dictionary<string, string?>
                {
                    ["email"] = user.Email,
                    ["token"] = encodedToken
                }
            );

            await _emailService.SendPasswordResetEmail(
                user.Email!,
                resetLink
            );
    }

    public async Task<IdentityResult> ResetPassword(ResetPasswordDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null)
            return IdentityResult.Failed();

        string token;
        try
        {
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(dto.Token));
        }
        catch (FormatException)
        {
            return IdentityResult.Failed();
        }


        return await _userManager.ResetPasswordAsync(
            user,
            token,
            dto.NewPassword
        );
    }

    public async Task SendConfirmationEmail(AppUser user)
    {
        if (user.EmailConfirmed)
            return;

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        var frontendUrl = _configuration["Frontend:BaseUrl"];

            var confirmEmailLink = QueryHelpers.AddQueryString(
                $"{frontendUrl}/confirm-email",
                new Dictionary<string, string?>
                {
                    ["email"] = user.Email,
                    ["token"] = encodedToken
                }
            );

        await _emailService.SendConfirmationEmail(user.Email!, confirmEmailLink);
    }

    public async Task ResendConfirmationEmail(ClaimsPrincipal principal)
    {
        var user = await _userManager.GetUserAsync(principal);
        if (user is null)
            throw new UnauthorizedAccessException();
        
        await SendConfirmationEmail(user);
    }


    public async Task<IdentityResult> ConfirmEmail(ConfirmEmailDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        if (user is null)
            return IdentityResult.Failed();

        string token;
        try
        {
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(dto.Token));
        }
        catch (FormatException)
        {
            return IdentityResult.Failed();
        }

        return await _userManager.ConfirmEmailAsync(
            user,
            token
        );
    }
}