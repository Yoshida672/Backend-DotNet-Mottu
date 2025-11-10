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
    [Route("api/v{version:apiVersion}/tag")]
    [ApiController]
    [ApiVersion("1.0")]
    [SwaggerTag("Gerencia as operações das tags UWB, incluindo cadastro, atualização, exclusão e listagem.")]
    public class TagUwbController(
        IUseCase<TagUwb, CreateTagUwb, UpdateTagUwbRequest, TagUwbResponse> useCase,
        IValidator<CreateTagUwb> validator
    ) : ControllerBase
    {
        private readonly IUseCase<TagUwb, CreateTagUwb, UpdateTagUwbRequest, TagUwbResponse> _useCase = useCase;
        private readonly IValidator<CreateTagUwb> _validator = validator;

        [HttpGet]
        [SwaggerOperation(Summary = "Retorna todas as tags UWB cadastradas.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Lista de tags retornada com sucesso.", typeof(IEnumerable<TagUwbResponse>))]
        public async Task<IActionResult> GetAll()
        {

                var result = await _useCase.GetAllAsync();
                return Ok(result);

        }

        [HttpGet("{id:long}")]
        [SwaggerOperation(Summary = "Retorna uma tag UWB pelo seu Id.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Tag encontrada.", typeof(TagUwbResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Tag não encontrada.")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _useCase.GetByIdAsync(id);
            if (result == null) return NotFound("Tag não encontrada.");
            return Ok(result);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova tag UWB.")]
        [SwaggerResponse((int)HttpStatusCode.Created, "Tag criada com sucesso.", typeof(TagUwbResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Dados inválidos.")]
        public async Task<IActionResult> Create([FromBody] CreateTagUwb request)
        {
            _validator.ValidateAndThrow(request);
            var response = await _useCase.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id:long}")]
        [SwaggerOperation(Summary = "Atualiza uma tag UWB existente.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Tag atualizada com sucesso.", typeof(TagUwbResponse))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Dados inválidos.")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Tag não encontrada.")]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateTagUwbRequest request)
        {
            var updated = await _useCase.UpdateAsync(id, request);
            if (updated == null) return NotFound("Tag não encontrada para atualização.");
            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        [SwaggerOperation(Summary = "Exclui uma tag UWB pelo Id.")]
        [SwaggerResponse((int)HttpStatusCode.NoContent, "Tag excluída com sucesso.")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Tag não encontrada.")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _useCase.DeleteAsync(id);
            if (!deleted) return NotFound("Tag não encontrada para exclusão.");
            return NoContent();
        }

        [HttpGet("paginado")]
        [SwaggerOperation(Summary = "Retorna tags UWB com paginação.")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Resultado paginado retornado com sucesso.", typeof(PaginatedResult<TagUwbResponse>))]
        public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await (_useCase as TagUwbUseCase)?.GetPaginatedAsync(page, pageSize);
            if (result == null)
                return StatusCode(500, "UseCase não suporta paginação ou não foi convertido corretamente.");

            return Ok(result);
        }
    }
}
