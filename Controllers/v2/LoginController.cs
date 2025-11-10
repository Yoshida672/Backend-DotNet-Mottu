using System.Net;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend_Dotnet_Mottu.Application.DTOs.Request;
using Backend_Dotnet_Mottu.Application.DTOs.Response;
using Backend_Dotnet_Mottu.Application.UseCases;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend_Dotnet_Mottu.API.Controllers
{
    [Route("api/v{version:apiVersion}/login")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [SwaggerTag("Gerenciamento de Login")]
    [AllowAnonymous]
    public class LoginController(ILoginUseCase loginUseCase) : ControllerBase
    {
        [HttpPost]
        public async Task<LoginResponse> Login([FromBody] LoginRequest loginDto)
        {
          return await loginUseCase.Login(loginDto);
        }
    }
}
