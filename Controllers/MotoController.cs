using FluentValidation;
using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Application.UseCases;
using CP2_BackEndMottu_DotNet.Application.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using CP2_BackEndMottu_DotNet.Infrastructure.Context;

namespace CP2_BackEndMottu_DotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotoController : ControllerBase
    {
        private readonly IRepository<Moto> _repository;
        private readonly MotoUseCase _useCase;
        private readonly CreateMotoRequestValidator _validator;
        private readonly MotoContext _context;
        public MotoController(IRepository<Moto> repository, MotoUseCase useCase, CreateMotoRequestValidator validator,MotoContext context)
        {
            _repository = repository;
            _useCase = useCase;
            _validator = validator;
            _context = context;
        }

        /// <summary>
        /// Lista todas as motos cadastradas.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MotoResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MotoResponse>>> GetMotos()
        {
            var motos = await _useCase.GetAllAsync();
            return Ok(motos);
        }

        /// <summary>
        /// Retorna uma moto específica pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MotoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<MotoResponse>> GetMoto(Guid id)
        {
            var moto = await _useCase.GetByIdAsync(id);
            if (moto == null)
                return NotFound();

            return Ok(moto);
        }

        /// <summary>
        /// Cria uma nova moto.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(MotoResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MotoResponse>> PostMoto(CreateMoto request)
        {
            _validator.ValidateAndThrow(request);
            var moto = await _useCase.CreateAsync(request);

            return CreatedAtAction(nameof(GetMoto), new { id = moto.Id }, moto);
        }

        /// <summary>
        /// Atualiza uma moto existente.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> PutMoto(Guid id, UpdateMotoRequest moto)
        {
            if (id != moto.Id)
                return BadRequest("Id inválido.");

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();
            existing.AtualizarDados(moto.Placa, moto.Modelo, moto.Status);

            _repository.Update(existing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

            /// <summary>
            /// Exclui uma moto pelo ID.
            /// </summary>
            [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteMoto(Guid id)
        {
            Moto moto = await _repository.GetByIdAsync(id);
            if (moto == null)
                return NotFound();

            _repository.Delete(moto);
            return NoContent();
        }
    }
}
