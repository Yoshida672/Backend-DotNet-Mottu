using Asp.Versioning;
using Backend_Dotnet_Mottu.Application.DTOs.Request;
using Backend_Dotnet_Mottu.Application.DTOs.Response;
using Backend_Dotnet_Mottu.Application.UseCases;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend_Dotnet_Mottu.Controllers
{
    [Route("api/v{version:apiVersion}/localizacao")]
    [ApiController]
    [ApiVersion("1.0")]
    [SwaggerTag("Gerencia o cadastro e listagem de localizações.")]
    public class LocalizacaoController(
        ILocalizacaoUseCase useCase,
        IValidator<CreateLocalizacaoUwb> validator
    ) : ControllerBase
    {
        private readonly ILocalizacaoUseCase _useCase = useCase;
        private readonly IValidator<CreateLocalizacaoUwb> _validator = validator;

        [HttpGet]
        [SwaggerOperation(Summary = "Retorna todas as localizações.", Description = "Lista todas as localizações registradas.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista retornada com sucesso.")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _useCase.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [SwaggerOperation(Summary = "Retorna uma localização pelo ID.", Description = "Busca uma localização específica pelo identificador.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Localização encontrada.", typeof(LocalizacaoResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Localização não encontrada.")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _useCase.GetByIdAsync(id);
            if (result == null) return NotFound("Localização não encontrada.");
            return Ok(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova localização.", Description = "Permite registrar uma nova localização.")]
        [SwaggerResponse(StatusCodes.Status201Created, "Localização criada com sucesso.", typeof(LocalizacaoResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Dados inválidos.")]
        public async Task<IActionResult> Create([FromBody] CreateLocalizacaoUwb request)
        {
            _validator.ValidateAndThrow(request);
            var response = await _useCase.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id:long}")]
        [SwaggerOperation(Summary = "Atualiza uma localização existente.", Description = "Permite atualizar os dados de uma localização.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Localização atualizada.", typeof(LocalizacaoResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Localização não encontrada.")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateLocalizacaoRequest request)
        {
            var updated = await _useCase.UpdateAsync(id, request);
            if (updated == null) return NotFound("Localização não encontrada para atualização.");
            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        [SwaggerOperation(Summary = "Exclui uma localização.", Description = "Permite excluir uma localização pelo ID.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Localização excluída com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Localização não encontrada.")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _useCase.DeleteAsync(id);
            if (!deleted) return NotFound("Localização não encontrada para exclusão.");
            return NoContent();
        }

        [HttpGet("paginado")]
        [SwaggerOperation(Summary = "Retorna localizações paginadas.", Description = "Lista localizações registradas com paginação.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Lista paginada retornada com sucesso.")]
        public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var resultado = await (_useCase as LocalizacaoUseCase)?.GetPaginatedAsync(page, pageSize);
            if (resultado == null)
                return StatusCode(500, "UseCase não suporta paginação ou não foi convertido corretamente.");

            return Ok(resultado);
        }
    }
}
