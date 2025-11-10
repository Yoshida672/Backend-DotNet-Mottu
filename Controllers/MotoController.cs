using System.Net;
using Asp.Versioning;
using Backend_Dotnet_Mottu.Application.DTOs.Request;
using Backend_Dotnet_Mottu.Application.DTOs.Response;
using Backend_Dotnet_Mottu.Application.UseCases;
using Backend_Dotnet_Mottu.Domain.Entities;
using Backend_Dotnet_Mottu.Domain.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend_Dotnet_Mottu.Controllers
{
    [Route("api/v{version:apiVersion}/moto")]
    [ApiController]
    [ApiVersion("1.0")]
    [SwaggerTag("Gerencia as operações de motos, incluindo cadastro, atualização, exclusão e listagem.")]
    public class MotoController(
        IUseCase<Moto, CreateMoto, UpdateMotoRequest, MotoResponse> useCase,
        IValidator<CreateMoto> validator
    ) : ControllerBase
    {
        private readonly IUseCase<Moto, CreateMoto, UpdateMotoRequest, MotoResponse> _useCase = useCase;
        private readonly IValidator<CreateMoto> _validator = validator;

        [HttpGet]
        [SwaggerOperation(Summary = "Retorna todas as motos cadastradas.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Lista de motos retornada com sucesso.", typeof(IEnumerable<MotoResponse>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _useCase.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [SwaggerOperation(Summary = "Retorna uma moto pelo seu Id.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Moto encontrada.", typeof(MotoResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Moto não encontrada.")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _useCase.GetByIdAsync(id);
            if (result == null) return NotFound("Moto não encontrada.");
            return Ok(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova moto.")]
        [SwaggerResponse((int)HttpStatusCode.Created, "Moto criada com sucesso.", typeof(MotoResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Dados inválidos.")]
        public async Task<IActionResult> Create([FromBody] CreateMoto request)
        {
            _validator.ValidateAndThrow(request);
            var response = await _useCase.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id:long}")]
        [SwaggerOperation(Summary = "Atualiza uma moto existente.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Moto atualizada com sucesso.", typeof(MotoResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Dados inválidos.")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Moto não encontrada.")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateMotoRequest request)
        {
            var updated = await _useCase.UpdateAsync(id, request);
            if (updated == null) return NotFound("Moto não encontrada para atualização.");
            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        [SwaggerOperation(Summary = "Exclui uma moto pelo Id.")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "Moto excluída com sucesso.")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Moto não encontrada.")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _useCase.DeleteAsync(id);
            if (!deleted) return NotFound("Moto não encontrada para exclusão.");
            return NoContent();
        }

        [HttpGet("paginado")]
        [SwaggerOperation(Summary = "Retorna motos com paginação.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Resultado paginado retornado com sucesso.", typeof(PaginatedResult<MotoResponse>))]
        public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await (_useCase as MotoUseCase)?.GetPaginatedAsync(page, pageSize);
            if (result == null) return StatusCode(500, "UseCase não suporta paginação ou não foi convertido corretamente.");
            return Ok(result);
        }
    }
}
