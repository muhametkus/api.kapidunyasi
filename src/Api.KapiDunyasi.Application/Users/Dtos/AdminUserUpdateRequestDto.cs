using Api.KapiDunyasi.Domain.User;

namespace Api.KapiDunyasi.Application.Users.Dtos;

public class AdminUserUpdateRequestDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public UserRole? Role { get; set; }
}
