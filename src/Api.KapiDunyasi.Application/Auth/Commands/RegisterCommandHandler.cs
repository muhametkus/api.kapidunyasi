using Api.KapiDunyasi.Application.Auth.Dtos;
using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Auth.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public RegisterCommandHandler(
        IAppDbContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator tokenGenerator)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var email = request.Payload.Email.Trim().ToLowerInvariant();
        var exists = await _context.Users.AnyAsync(x => x.Email == email, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("Bu e-posta zaten kayitli.");
        }

        var user = new User(
            request.Payload.Name.Trim(),
            email,
            _passwordHasher.Hash(request.Payload.Password),
            UserRole.User);

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new AuthResponseDto
        {
            Token = _tokenGenerator.GenerateToken(user),
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }
}
