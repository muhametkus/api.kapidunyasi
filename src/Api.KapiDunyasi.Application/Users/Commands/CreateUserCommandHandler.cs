using Api.KapiDunyasi.Application.Common.Interfaces;
using Api.KapiDunyasi.Domain.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IAppDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var email = payload.Email.Trim().ToLowerInvariant();
        
        var exists = await _context.Users.AnyAsync(x => x.Email == email, cancellationToken);
        if (exists)
        {
            throw new InvalidOperationException("Bu e-posta zaten kayitli.");
        }

        var user = new User(
            payload.Name.Trim(),
            email,
            _passwordHasher.Hash(payload.Password),
            payload.Role);

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
