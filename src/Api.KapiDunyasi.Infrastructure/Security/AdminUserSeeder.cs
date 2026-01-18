using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Api.KapiDunyasi.Infrastructure.Security;

public class AdminUserSeeder
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IConfiguration _configuration;

    public AdminUserSeeder(
        IAppDbContext context,
        IPasswordHasher passwordHasher,
        IConfiguration configuration)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        var adminSection = _configuration.GetSection("AdminUser");
        var email = adminSection["Email"];
        var password = adminSection["Password"];
        var name = adminSection["Name"] ?? "Admin";

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return;
        }

        email = email.Trim().ToLowerInvariant();
        var exists = await _context.Users.AnyAsync(x => x.Email == email, cancellationToken);
        if (exists)
        {
            return;
        }

        var admin = new User(name, email, _passwordHasher.Hash(password), UserRole.Admin);
        _context.Users.Add(admin);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
