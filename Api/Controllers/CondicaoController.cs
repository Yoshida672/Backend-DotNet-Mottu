using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
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

    /// <summary>
    /// Retorna todas as condições registradas.
    /// </summary>
    /// <returns>Lista de CondicaoResponse</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var condicoes = await _useCase.GetAllAsync();
        return Ok(condicoes);
    }

    /// <summary>
    /// Retorna uma condição específica pelo ID.
    /// </summary>
    /// <param name="id">ID da condição</param>
    /// <returns>Objeto CondicaoResponse correspondente</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var condicao = await _useCase.GetByIdAsync(id);
        if (condicao == null) return NotFound("Condição não encontrada.");
        return Ok(condicao);
    }

    /// <summary>
    /// Cria uma nova condição.
    /// </summary>
    /// <param name="request">Dados para criação da condição</param>
    /// <returns>Objeto CondicaoResponse criado</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCondicaoRequest request)
    {
        var result = await _useCase.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Atualiza uma condição existente pelo ID.
    /// </summary>
    /// <param name="id">ID da condição a ser atualizada</param>
    /// <param name="request">Dados atualizados da condição</param>
    /// <returns>Objeto CondicaoResponse atualizado</returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCondicaoRequest request)
    {
        var updated = await _useCase.UpdateAsync(id, request);
        if (updated == null) return NotFound("Condição não encontrada para atualização.");
        return Ok(updated);
    }

    /// <summary>
    /// Exclui uma condição pelo ID.
    /// </summary>
    /// <param name="id">ID da condição a ser excluída</param>
    /// <returns>Status da operação</returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _useCase.DeleteAsync(id);
        if (!deleted) return NotFound("Condição não encontrada para exclusão.");
        return NoContent();
    }
}
