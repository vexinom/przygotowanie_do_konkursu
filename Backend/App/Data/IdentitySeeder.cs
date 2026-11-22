using Microsoft.AspNetCore.Identity;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

        await SeedRolesAsync(roleManager);
        await SeedUsersAsync(userManager);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
    {
        string[] roles =
        {
            Roles.Student,
            Roles.Teacher,
            Roles.Admin
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(
                    new IdentityRole<Guid>(role));

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        $"Nie udało się utworzyć roli {role}: {FormatErrors(result)}");
                }
            }
        }
    }

    private static async Task SeedUsersAsync(UserManager<AppUser> userManager)
    {
        await EnsureUserAsync(userManager, "admin", "admin@amu.edu.pl", "Admin123!", Roles.Admin);
        await EnsureUserAsync(userManager, "nauczyciel", "nauczyciel@amu.edu.pl", "Teacher123!", Roles.Teacher);
        await EnsureUserAsync(userManager, "uczen", "uczen@amu.edu.pl", "Student123!", Roles.Student);
    }

    private static async Task EnsureUserAsync(
        UserManager<AppUser> userManager, string username,  string email, string password, string role)
    {
        var existingUser = await userManager.FindByEmailAsync(email);
        if (existingUser is not null)
            return;

        var user = new AppUser
        {
            UserName = username,
            Email = email,
            EmailConfirmed = true
        };

        var createResult = await userManager.CreateAsync(user, password);
        if (!createResult.Succeeded)
        {
            throw new InvalidOperationException(
                $"Nie udało się utworzyć użytkownika {email}: {FormatErrors(createResult)}");
        }

        var roleResult = await userManager.AddToRoleAsync(user, role);
        if (!roleResult.Succeeded)
        {
            throw new InvalidOperationException(
                $"Nie udało się przypisać roli {role} użytkownikowi {email}: {FormatErrors(roleResult)}");
        }
    }

    private static string FormatErrors(IdentityResult result) =>
        string.Join(", ", result.Errors.Select(e => e.Description));    
}
