using Api.KapiDunyasi.Domain.User;

namespace Api.KapiDunyasi.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
