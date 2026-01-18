using Api.KapiDunyasi.Domain.Common;

namespace Api.KapiDunyasi.Domain.User;

public class User : AggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }

    protected User() { }

    public User(string name, string email, string passwordHash, UserRole role)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public void UpdateProfile(string? name, string? email)
    {
        if (!string.IsNullOrWhiteSpace(name))
            Name = name.Trim();
        if (!string.IsNullOrWhiteSpace(email))
            Email = email.Trim().ToLowerInvariant();
        SetUpdated();
    }

    public void UpdatePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        SetUpdated();
    }

    public void UpdateRole(UserRole role)
    {
        Role = role;
        SetUpdated();
    }
}
