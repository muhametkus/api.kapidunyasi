using Api.KapiDunyasi.Domain.User;

namespace Api.KapiDunyasi.Application.Users.Dtos;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
