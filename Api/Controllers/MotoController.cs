using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Domain.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CP2_BackEndMottu_DotNet.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly IUseCase<Moto, CreateMoto, UpdateMotoRequest, MotoResponse> _motoUseCase;

        public MotoController(IUseCase<Moto, CreateMoto, UpdateMotoRequest, MotoResponse> motoUseCase)
        {
            _motoUseCase = motoUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var motos = await _motoUseCase.GetAllAsync();
            return Ok(motos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var moto = await _motoUseCase.GetByIdAsync(id);
            if (moto == null)
                return NotFound();
            return Ok(moto);
        }

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _motoUseCase.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
