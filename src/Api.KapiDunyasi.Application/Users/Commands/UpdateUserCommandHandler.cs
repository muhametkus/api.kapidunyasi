using Api.KapiDunyasi.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.KapiDunyasi.Application.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IAppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public UpdateUserCommandHandler(IAppDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var payload = request.Payload;
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (user == null)
        {
            throw new InvalidOperationException("Kullanici bulunamadi.");
        }

        // Kullanıcı sadece kendi profilini güncelleyebilir
        if (user.Id != request.RequestingUserId)
        {
            throw new UnauthorizedAccessException("Bu islemi yapmaya yetkiniz yok.");
        }

        user.UpdateProfile(payload.Name, payload.Email);

        if (!string.IsNullOrWhiteSpace(payload.Password))
        {
            user.UpdatePassword(_passwordHasher.Hash(payload.Password));
        }

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
