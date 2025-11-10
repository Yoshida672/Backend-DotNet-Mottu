using Backend_Dotnet_Mottu.Application;
using Backend_Dotnet_Mottu.Application.DTOs.Request;
using Backend_Dotnet_Mottu.Application.DTOs.Response;
using Backend_Dotnet_Mottu.Application.UseCases;
using Backend_Dotnet_Mottu.Domain.Entities;
using Backend_Dotnet_Mottu.Domain.Pagination;


public class CondicaoUseCase : IUseCase<Condicao, CreateCondicaoRequest, UpdateCondicaoRequest, CondicaoResponse>
{
    private readonly IRepository<Condicao> _repository;

    public CondicaoUseCase(IRepository<Condicao> repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<CondicaoResponse>> GetPaginatedAsync(int page, int pageSize)
    {
        var (items, totalItems) = await _repository.GetPaginatedAsync(page, pageSize);

        var responseItems = items.Select(c => new CondicaoResponse
        {
            Id = c.Id,
            Nome = c.Nome,
            Cor = c.Cor
        }).ToList();

        return new PaginatedResult<CondicaoResponse>
        {
            Items = responseItems,
            TotalItems = totalItems,
            Page = page,
            PageSize = pageSize
        };
    }


    public async Task<IEnumerable<CondicaoResponse>> GetAllAsync()
    {
        var condicoes = await _repository.GetAllAsync();
        return condicoes.Select(c => new CondicaoResponse
        {
            Id = c.Id,
            Nome = c.Nome,
            Cor = c.Cor
        });
    }

    public async Task<CondicaoResponse?> GetByIdAsync(long id)
    {
        var condicao = await _repository.GetByIdAsync(id);
        if (condicao == null) return null;

        return new CondicaoResponse
        {
            Id = condicao.Id,
            Nome = condicao.Nome,
            Cor = condicao.Cor
        };
    }

    public async Task<CondicaoResponse> CreateAsync(CreateCondicaoRequest request)
    {
        var nova = new Condicao(request.Nome, request.Cor);
        await _repository.AddAsync(nova);

        return new CondicaoResponse
        {
            Id = nova.Id,
            Nome = nova.Nome,
            Cor = nova.Cor
        };
    }

    public async Task<CondicaoResponse?> UpdateAsync(long id, UpdateCondicaoRequest request)
    {
        var condicao = await _repository.GetByIdAsync(id);
        if (condicao == null) return null;

        condicao.Nome = request.Nome;
        condicao.Cor = request.Cor;

        await _repository.UpdateAsync(condicao);

        return new CondicaoResponse
        {
            Id = condicao.Id,
            Nome = condicao.Nome,
            Cor = condicao.Cor
        };
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var condicao = await _repository.GetByIdAsync(id);
        if (condicao == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}
