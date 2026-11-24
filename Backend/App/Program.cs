using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Services;
using Microsoft.AspNetCore.Identity;
using MailKit.Security;
using FluentEmail.MailKitSmtp;
using Backend.Exceptions;
using Backend.Mapping;
using Backend.Models;


var builder = WebApplication.CreateBuilder(args);


//pobranie connectionStringa z dotnet User Secrets 
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

//dodawnie kontrolerów i serwisów
builder.Services.AddControllers();

builder.Services.AddScoped<IContestService, ContestService>();
builder.Services.AddScoped<IProblemService, ProblemService>();
builder.Services.AddScoped<IUserProgressService, UserProgressService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddExceptionHandler<AccessExceptionHandler>();
builder.Services.AddScoped<ISolvingService, SolvingService>();
builder.Services.AddScoped<IProblemSelector, RandomProblemSelector>(); 

builder.Services.AddProblemDetails(); //do struktury zwracanych błędów https
builder.Services.AddOpenApi();


// dodanie mapowania
builder.Services.AddAutoMapper(cfg =>
{
    cfg.LicenseKey = builder.Configuration["AutoMapper:LicenseKey"];
    cfg.AddMaps(typeof(MappingProfile).Assembly);
});



// Dodanie obsługi ról użytkowników
builder.Services
     .AddIdentity<AppUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


//ustawienia haseł do logowania użytkowników
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;

    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
});

//ustawienia wysyłania emaili
builder.Services
    .AddFluentEmail(
        builder.Configuration["Email:SenderEmail"]!,
        builder.Configuration["Email:SenderName"]
    )
    .AddMailKitSender(new SmtpClientOptions
    {
        Server = builder.Configuration["Email:Host"]!,
        Port = builder.Configuration.GetValue<int>("Email:Port"),
        User = builder.Configuration["Email:Username"],
        Password = builder.Configuration["Email:Password"],
        RequiresAuthentication = true,
        SocketOptions = SecureSocketOptions.StartTls
    });

// ustawienia bezpieczeńśtwa tokenów
builder.Services.Configure<DataProtectionTokenProviderOptions>( 
    options =>
    {
        options.TokenLifespan = TimeSpan.FromHours(1); //czas ważności tokenu do resetu hasła
    });

// ustawienia ciasteczek aplikacji
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax;

    options.ExpireTimeSpan = TimeSpan.FromHours(2);
    options.SlidingExpiration = true;
});


// ustawienia CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


// ===============================================================================================
// budowa aplikacji
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

using (var scope = app.Services.CreateScope())
{
    await DataSeeder.SeedAsync(scope.ServiceProvider);
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler();
app.MapControllers();

app.Run();

