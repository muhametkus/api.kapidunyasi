namespace Api.KapiDunyasi.Application.Auth.Dtos;

public class LoginRequestDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
