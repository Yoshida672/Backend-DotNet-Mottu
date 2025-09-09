using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Domain.Interface;

namespace CP2_BackEndMottu_DotNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("CRUD Localização UWB")]
    public class LocalizacaoController : ControllerBase
    {
        private readonly IUseCase<LocalizacaoUWB, CreateLocalizacaoUwb, UpdateLocalizacaoRequest, LocalizacaoResponse> _useCase;
        private readonly IValidator<CreateLocalizacaoUwb> _validator;

        public LocalizacaoController(
            IUseCase<LocalizacaoUWB, CreateLocalizacaoUwb, UpdateLocalizacaoRequest, LocalizacaoResponse> useCase,
            IValidator<CreateLocalizacaoUwb> validator)
        {
            _useCase = useCase;
            _validator = validator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LocalizacaoResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<LocalizacaoResponse>>> GetAll()
        {
            var result = await _useCase.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LocalizacaoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<LocalizacaoResponse>> GetById(Guid id)
        {
            var result = await _useCase.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(LocalizacaoResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<LocalizacaoResponse>> Post(CreateLocalizacaoUwb request)
        {
            _validator.ValidateAndThrow(request);
            var response = await _useCase.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(LocalizacaoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(Guid id, UpdateLocalizacaoRequest request)
        {
            var updated = await _useCase.UpdateAsync(id, request);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _useCase.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
