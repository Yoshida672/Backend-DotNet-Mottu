using Backend_Dotnet_Mottu.Application.DTOs.Request;
using Backend_Dotnet_Mottu.Application.DTOs.Response;

namespace Backend_Dotnet_Mottu.Application.UseCases;

public interface ILoginUseCase
{
    Task<LoginResponse> Login(LoginRequest request);
}