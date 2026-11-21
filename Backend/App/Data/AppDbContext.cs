using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Backend.Models;

namespace Backend.Data;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Problem> Problems { get; set; } = default!;
    public DbSet<Answer> Answers { get; set; } = default!;
    public DbSet<Contest> Contests { get; set; } = default!;
    public DbSet<HistoryEntry> History { get; set; } = default!;
}
