using Asp.Versioning;
using Backend_Dotnet_Mottu.Application.DTOs.Request;
using Backend_Dotnet_Mottu.Application.DTOs.Response;
using Backend_Dotnet_Mottu.Application.UseCases;
using Backend_Dotnet_Mottu.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend_Dotnet_Mottu.Controllers
{
    [Route("api/v{version:apiVersion}/condicao")]
    [ApiController]
    [ApiVersion("1.0")]
    [SwaggerTag("Gerencia o cadastro e listagem de condições.")]
    public class CondicaoController(
        IUseCase<Condicao, CreateCondicaoRequest, UpdateCondicaoRequest, CondicaoResponse> useCase,
        IValidator<CreateCondicaoRequest> validator
    ) : ControllerBase
    {
        private readonly IUseCase<Condicao, CreateCondicaoRequest, UpdateCondicaoRequest, CondicaoResponse> _useCase = useCase;
        private readonly IValidator<CreateCondicaoRequest> _validator = validator;

        [HttpGet("paginado")]
        [SwaggerOperation(Summary = "Retorna condições paginadas.", Description = "Lista condições registradas com paginação.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista retornada com sucesso.")]
        public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var resultado = await (_useCase as CondicaoUseCase)?.GetPaginatedAsync(page, pageSize);
            if (resultado == null)
                return StatusCode(500, "UseCase não suporta paginação ou não foi convertido corretamente.");
            return Ok(resultado);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Retorna todas as condições.", Description = "Lista todas as condições cadastradas.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista retornada com sucesso.")]
        public async Task<IActionResult> GetAll()
        {
            var condicoes = await _useCase.GetAllAsync();
            return Ok(condicoes);
        }

        [HttpGet("{id:long}")]
        [SwaggerOperation(Summary = "Retorna uma condição pelo ID.", Description = "Busca uma condição específica pelo seu identificador.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Condição encontrada.", typeof(CondicaoResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Condição não encontrada.")]
        public async Task<IActionResult> GetById(long id)
        {
            var condicao = await _useCase.GetByIdAsync(id);
            if (condicao == null) return NotFound("Condição não encontrada.");
            return Ok(condicao);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova condição.", Description = "Permite registrar uma nova condição.")]
        [SwaggerResponse(StatusCodes.Status201Created, "Condição criada com sucesso.", typeof(CondicaoResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos.")]
        public async Task<IActionResult> Create([FromBody] CreateCondicaoRequest request)
        {
            var result = await _useCase.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:long}")]
        [SwaggerOperation(Summary = "Atualiza uma condição existente.", Description = "Permite atualizar os dados de uma condição.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Condição atualizada.", typeof(CondicaoResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Condição não encontrada.")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateCondicaoRequest request)
        {
            var updated = await _useCase.UpdateAsync(id, request);
            if (updated == null) return NotFound("Condição não encontrada para atualização.");
            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        [SwaggerOperation(Summary = "Exclui uma condição.", Description = "Permite excluir uma condição pelo ID.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Condição excluída com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Condição não encontrada.")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _useCase.DeleteAsync(id);
            if (!deleted) return NotFound("Condição não encontrada para exclusão.");
            return NoContent();
        }
    }
}
