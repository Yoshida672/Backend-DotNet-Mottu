using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Application.UseCases.impl;
using CP2_BackEndMottu_DotNet.Application.UseCases;
using CP2_BackEndMottu_DotNet.Domain.Pagination;

namespace CP2_BackEndMottu_DotNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Motos")]
    public class MotoController(
        IUseCase<Moto, CreateMoto, UpdateMotoRequest, MotoResponse> useCase,
        IValidator<CreateMoto> validator) : ControllerBase
    {
        private readonly IUseCase<Moto, CreateMoto, UpdateMotoRequest, MotoResponse> _useCase = useCase;
        private readonly IValidator<CreateMoto> _validator = validator;

        /// <summary>
        /// Retorna todas as motos cadastradas.
        /// </summary>
        /// <returns>Lista de MotoResponse</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MotoResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MotoResponse>>> GetAll()
        {
            var result = await _useCase.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retorna uma moto pelo seu Id.
        /// </summary>
        /// <param name="id">Id da moto</param>
        /// <returns>Objeto MotoResponse</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MotoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<MotoResponse>> GetById(Guid id)
        {
            var result = await _useCase.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Cria uma nova moto.
        /// </summary>
        /// <param name="request">Objeto CreateMoto com os dados da moto</param>
        /// <returns>Objeto MotoResponse criado</returns>
        [HttpPost]
        [ProducesResponseType(typeof(MotoResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MotoResponse>> Post(CreateMoto request)
        {
            _validator.ValidateAndThrow(request);
            var response = await _useCase.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        /// <summary>
        /// Atualiza uma moto existente.
        /// </summary>
        /// <param name="id">Id da moto</param>
        /// <param name="request">Objeto UpdateMotoRequest com os dados atualizados</param>
        /// <returns>Objeto MotoResponse atualizado</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MotoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(Guid id, UpdateMotoRequest request)
        {
            var updated = await _useCase.UpdateAsync(id, request);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        /// <summary>
        /// Remove uma moto pelo Id.
        /// </summary>
        /// <param name="id">Id da moto</param>
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

        /// <summary>
        /// Retorna as motos com paginação.
        /// </summary>
        /// <param name="page">Número da página (padrão: 1)</param>
        /// <param name="pageSize">Tamanho da página (padrão: 10)</param>
        /// <returns>Resultado paginado de MotoResponse</returns>
        [HttpGet("paginado")]
        [ProducesResponseType(typeof(PaginatedResult<MotoResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await (_useCase as MotoUseCase)?.GetPaginatedAsync(page, pageSize);
            if (result == null)
                return StatusCode(500, "UseCase não suporta paginação ou não foi convertido corretamente.");
            return Ok(result);
        }
    }
}
