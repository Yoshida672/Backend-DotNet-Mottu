using System.Net;
using Asp.Versioning;
using Backend_Dotnet_Mottu.Application.DTOs.Request;
using Backend_Dotnet_Mottu.Application.DTOs.Response;
using Backend_Dotnet_Mottu.Application.UseCases;
using Backend_Dotnet_Mottu.Domain.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Backend_Dotnet_Mottu.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/localizacao")]
    [Tags("Localizacao")]
    [Authorize] 
    public class LocalizacaoControllerV2(
        ILocalizacaoUseCase useCase,
        IValidator<CreateLocalizacaoUwb> validator
    ) : ControllerBase
    {
        private readonly ILocalizacaoUseCase _useCase = useCase;
        private readonly IValidator<CreateLocalizacaoUwb> _validator = validator;

        [HttpGet]
        [Authorize] 
        [ProducesResponseType(typeof(IEnumerable<LocalizacaoResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _useCase.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        [Authorize]
        [ProducesResponseType(typeof(LocalizacaoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _useCase.GetByIdAsync(id);
            if (result == null) return NotFound("Localização não encontrada.");
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(LocalizacaoResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateLocalizacaoUwb request)
        {
            _validator.ValidateAndThrow(request);
            var response = await _useCase.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id:long}")]
        [Authorize]
        [ProducesResponseType(typeof(LocalizacaoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateLocalizacaoRequest request)
        {
            var updated = await _useCase.UpdateAsync(id, request);
            if (updated == null) return NotFound("Localização não encontrada para atualização.");
            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _useCase.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("paginado")]
        [Authorize]
        [ProducesResponseType(typeof(PaginatedResult<LocalizacaoResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await (_useCase as LocalizacaoUseCase)?.GetPaginatedAsync(page, pageSize);
            if (result == null)
                return StatusCode(500, "UseCase não suporta paginação ou não foi convertido corretamente.");
            return Ok(result);
        }
    }
}
