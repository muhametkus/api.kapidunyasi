using Api.KapiDunyasi.Application.Auth.Dtos;
using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Auth.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public LoginCommandHandler(
        IAppDbContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator tokenGenerator)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var email = request.Payload.Email.Trim().ToLowerInvariant();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        if (user == null || !_passwordHasher.Verify(user.PasswordHash, request.Payload.Password))
        {
            throw new InvalidOperationException("E-posta veya sifre hatali.");
        }

        return new AuthResponseDto
        {
            Token = _tokenGenerator.GenerateToken(user),
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }
}
