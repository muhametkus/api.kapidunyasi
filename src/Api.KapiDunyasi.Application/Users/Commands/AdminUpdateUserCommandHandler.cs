using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Users.Commands;

public class AdminUpdateUserCommandHandler : IRequestHandler<AdminUpdateUserCommand, Unit>
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public AdminUpdateUserCommandHandler(IAppDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Unit> Handle(AdminUpdateUserCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (user == null)
        {
            throw new InvalidOperationException("Kullanici bulunamadi.");
        }

        user.UpdateProfile(payload.Name, payload.Email);

        if (!string.IsNullOrWhiteSpace(payload.Password))
        {
            user.UpdatePassword(_passwordHasher.Hash(payload.Password));
        }

        if (payload.Role.HasValue)
        {
            user.UpdateRole(payload.Role.Value);
        }

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
