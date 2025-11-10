using Backend_Dotnet_Mottu.Application.DTOs.Request;
using Backend_Dotnet_Mottu.Application.DTOs.Response;
using Backend_Dotnet_Mottu.Application.Services;
using Backend_Dotnet_Mottu.Infrastructure.Persistence;

namespace Backend_Dotnet_Mottu.Application.UseCases;

public class LoginUseCase(IUserRepository userRepository, ITokenService tokenService) : ILoginUseCase
{
    public Task<LoginResponse> Login(LoginRequest request)
    {
        var user = userRepository.GetUserByEmailAsync(request.Email);

        if (user is null || !user.VerifyPassword(request.Password))
            return Task.FromResult(new LoginResponse("Invalid credentials or blocked account"));

        return Task.FromResult(new LoginResponse
        {
            Token = tokenService.GenerateToken(user),
            Id = user.Id
        });
    }
}