using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Application.UseCases;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Domain.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CondicaoController : ControllerBase
{
    private readonly IUseCase<Condicao, CreateCondicaoRequest, UpdateCondicaoRequest, CondicaoResponse> _useCase;
    private readonly IValidator<CreateCondicaoRequest> _validator;

    public CondicaoController(

    IUseCase<Condicao, CreateCondicaoRequest, UpdateCondicaoRequest, CondicaoResponse> useCase,
    IValidator<CreateCondicaoRequest> validator
        )
        {
         _useCase = useCase;
        _validator = validator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var condicoes = await _useCase.GetAllAsync();
        return Ok(condicoes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var condicao = await _useCase.GetByIdAsync(id);
        if (condicao == null) return NotFound("Condição não encontrada.");
        return Ok(condicao);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCondicaoRequest request)
    {
        var result = await _useCase.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCondicaoRequest request)
    {
        var updated = await _useCase.UpdateAsync(id, request);
        if (updated == null) return NotFound("Condição não encontrada para atualização.");
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _useCase.DeleteAsync(id);
        if (!deleted) return NotFound("Condição não encontrada para exclusão.");
        return NoContent();
    }
}
