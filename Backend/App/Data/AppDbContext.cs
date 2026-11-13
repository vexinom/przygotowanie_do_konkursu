using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Backend.Models;

namespace Backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Problem> Problems { get; set; } = default!;
    public DbSet<Answer> Answers { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;

}