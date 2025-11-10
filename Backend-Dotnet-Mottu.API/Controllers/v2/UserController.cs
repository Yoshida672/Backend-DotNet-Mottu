using System.Net;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend_Dotnet_Mottu.Application.DTOs.Request;
using Backend_Dotnet_Mottu.Infrastructure.Persistence;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend_Dotnet_Mottu.Controllers.v2;


[Route("api/v{version:apiVersion}/users")]
[ApiController]
[ApiVersion("1.0")]
[Produces("application/json")]
[SwaggerTag("Gerenciamento de Usuários")]
public class UserController(IUserRepository userRepository) : ControllerBase
{
    
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, "Endpoint para criação de usuário")]
    public async Task<IActionResult> Post(CreateUserRequest createUserRequest)
    {
        await userRepository.AddUserAsync(createUserRequest.ToDomain());
        return StatusCode((int)HttpStatusCode.Created);
    }
}