using Backend_Dotnet_Mottu.Domain.Entities;

namespace Backend_Dotnet_Mottu.Application.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}