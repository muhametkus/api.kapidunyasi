using Api.KapiDunyasi.Domain.User;

namespace Api.KapiDunyasi.Application.Users.Dtos;

public class UserCreateRequestDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserRole Role { get; set; } = UserRole.User;
}
