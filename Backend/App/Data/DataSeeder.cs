using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;
public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
    var context = serviceProvider.GetRequiredService<AppDbContext>();

    await SeedRolesAsync(roleManager);
    await SeedUsersAsync(userManager);
    await SeedProblemsAsync(context);
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

    private static async Task SeedProblemsAsync(AppDbContext context)
    {
        if (await context.Problems.AnyAsync(p => !p.IsDeleted))
            return;

        var math = await EnsureContestAsync(context,
            "Konkurs matematyczny", "Zadania z arytmetyki, geometrii i analizy.");
        var cs = await EnsureContestAsync(context,
            "Konkurs informatyczny", "Zadania z algorytmów i struktur danych.");

        context.Problems.AddRange(
            NewProblem(math, "Ile wynosi 7 · 8?",
                ("54", false), ("56", true), ("58", false), ("64", false)),

            NewProblem(math, "Ile wynosi suma miar kątów wewnętrznych trójkąta?",
                ("90°", false), ("180°", true), ("270°", false), ("360°", false)),

            NewProblem(math, "Jaka jest pochodna funkcji f(x) = x²?",
                ("x", false), ("2x", true), ("x²", false), ("2", false)),

            NewProblem(cs, "Która struktura danych działa według zasady FIFO?",
                ("Stos", false), ("Kolejka", true), ("Drzewo BST", false), ("Zbiór", false)),

            NewProblem(cs, "Jaka jest złożoność wyszukiwania binarnego w posortowanej tablicy?",
                ("O(n)", false), ("O(n log n)", false), ("O(log n)", true), ("O(1)", false))
        );

        await context.SaveChangesAsync();
    }

    private static async Task<Contest> EnsureContestAsync(AppDbContext context, string name, string description)
    {
        var contest = await context.Contests
            .FirstOrDefaultAsync(c => !c.IsDeleted && c.Name == name);

        if (contest is not null)
            return contest;

        contest = new Contest { Name = name, Description = description };
        context.Contests.Add(contest);
        return contest;
    }

    private static Problem NewProblem(
        Contest contest, string content, params (string Content, bool IsCorrect)[] answers) => new()
    {
        Content = content,
        ContestId = contest.Id,
        Answers = answers
            .Select(a => new Answer { Content = a.Content, IsCorrect = a.IsCorrect })
            .ToList()
    };

    private static string FormatErrors(IdentityResult result) =>
        string.Join(", ", result.Errors.Select(e => e.Description));    
}
