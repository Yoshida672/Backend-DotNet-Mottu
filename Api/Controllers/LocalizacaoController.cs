using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Application.UseCases;
using CP2_BackEndMottu_DotNet.Application.UseCases.impl;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;

[ApiController]
[Route("api/[controller]")]
public class LocalizacaoController(
    ILocalizacaoUseCase useCase,
    IValidator<CreateLocalizacaoUwb> validator
) : ControllerBase
{
    private readonly ILocalizacaoUseCase _useCase = useCase;
    private readonly IValidator<CreateLocalizacaoUwb> _validator = validator;

    /// <summary>
    /// Retorna todas as localizações.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _useCase.GetAllAsync();
        return Ok(result);
    }

    /// <summary>
    /// Retorna uma localização pelo Id.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _useCase.GetByIdAsync(id);
        if (result == null) return NotFound("Localização não encontrada.");
        return Ok(result);
    }

    /// <summary>
    /// Cria uma nova localização.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLocalizacaoUwb request)
    {
        _validator.ValidateAndThrow(request);
        var response = await _useCase.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Atualiza uma localização existente.
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLocalizacaoRequest request)
    {
        var updated = await _useCase.UpdateAsync(id, request);
        if (updated == null) return NotFound("Localização não encontrada para atualização.");
        return Ok(updated);
    }

    /// <summary>
    /// Exclui uma localização pelo Id.
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _useCase.DeleteAsync(id);
        if (!deleted) return NotFound("Localização não encontrada para exclusão.");
        return NoContent();
    }

    /// <summary>
    /// Retorna as localizações com paginação.
    /// </summary>
    [HttpGet("paginado")]
    public async Task<IActionResult> GetPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var resultado = await (_useCase as LocalizacaoUseCase)?.GetPaginatedAsync(page, pageSize);
        if (resultado == null)
            return StatusCode(500, "UseCase não suporta paginação ou não foi convertido corretamente.");

        return Ok(resultado);

    }
}
