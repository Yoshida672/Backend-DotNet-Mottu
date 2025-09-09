using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CP2_BackEndMottu_DotNet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Moto")]
    public class MotoController : ControllerBase
    {
        private readonly IUseCase<Moto, CreateMoto, UpdateMotoRequest, MotoResponse> _motoUseCase;

        public MotoController(IUseCase<Moto, CreateMoto, UpdateMotoRequest, MotoResponse> motoUseCase)
        {
            _motoUseCase = motoUseCase;
        }

        /// <summary>
        /// Retorna todas as motos cadastradas.
        /// </summary>
        /// <returns>Lista de motos</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MotoResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var motos = await _motoUseCase.GetAllAsync();
            return Ok(motos);
        }

        /// <summary>
        /// Retorna uma moto pelo Id.
        /// </summary>
        /// <param name="id">Id da moto</param>
        /// <returns>Moto correspondente ao Id</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MotoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var moto = await _motoUseCase.GetByIdAsync(id);
            if (moto == null)
                return NotFound();
            return Ok(moto);
        }

        /// <summary>
        /// Cria uma nova moto.
        /// </summary>
        /// <param name="request">Dados da moto a ser criada</param>
        /// <returns>Moto criada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(MotoResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateMoto request)
        {
            try
            {
                var moto = await _motoUseCase.CreateAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = moto.Id }, moto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza uma moto pelo Id.
        /// </summary>
        /// <param name="id">Id da moto a ser atualizada</param>
        /// <param name="request">Dados atualizados da moto</param>
        /// <returns>Moto atualizada</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MotoResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMotoRequest request)
        {
            try
            {
                var moto = await _motoUseCase.UpdateAsync(id, request);
                if (moto == null)
                    return NotFound();
                return Ok(moto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove uma moto pelo Id.
        /// </summary>
        /// <param name="id">Id da moto a ser removida</param>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _motoUseCase.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
