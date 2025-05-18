using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Application.UseCases;
using CP2_BackEndMottu_DotNet.Application.Validators;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Infrastructure.Persistence.Repositories;

namespace CP2_BackEndMottu_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("CRUD Localização UWB")]
    public class LocalizacaoController : ControllerBase
    {
        private readonly IRepository<LocalizacaoUWB> _repository;
        private readonly LocalizacaoUseCase _useCase;
        private readonly CreateLocalizacaoRequestValidator _validator;

        public LocalizacaoController(
            IRepository<LocalizacaoUWB> repository,
            LocalizacaoUseCase useCase,
            CreateLocalizacaoRequestValidator validator)
        {
            _repository = repository;
            _useCase = useCase;
            _validator = validator;
        }

        /// <summary>
        /// Lista todas as localizações UWB
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LocalizacaoResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<LocalizacaoResponse>>> GetAll()
        {
            var result = await _useCase.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retorna uma localização UWB específica pelo ID
        /// </summary>
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

        /// <summary>
        /// Cadastra uma nova localização UWB
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(LocalizacaoResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<LocalizacaoResponse>> Post(CreateLocalizacaoUwb request)
        {
            _validator.ValidateAndThrow(request);

            var response = await _useCase.CreateAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        /// <summary>
        /// Atualiza uma localização UWB
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(Guid id, LocalizacaoUWB localizacao)
        {
            if (id != localizacao.Id)
                return BadRequest();

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            _repository.Update(localizacao);
            return NoContent();
        }

        /// <summary>
        /// Remove uma localização UWB
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var localizacao = await _repository.GetByIdAsync(id);
            if (localizacao == null)
                return NotFound();

            _repository.Delete(localizacao);
            return NoContent();
        }
    }
}
