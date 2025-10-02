using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Application.UseCases.impl;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Domain.Pagination;
using CP2_BackEndMottu_DotNet.Infrastructure.Persistence.Repositories.impl;

namespace CP2_BackEndMottu_DotNet.Application.UseCases
{
    public class LocalizacaoUseCase : ILocalizacaoUseCase
    {
        private readonly IRepository<LocalizacaoUWB> _repository;

        public LocalizacaoUseCase(IRepository<LocalizacaoUWB> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LocalizacaoUWB>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<LocalizacaoUWB?> GetByIdAsync(Guid id) =>
            await _repository.GetByIdAsync(id);

        public async Task<LocalizacaoUWB> CreateAsync(CreateLocalizacaoUwb request)
        {
            var localizacao = new LocalizacaoUWB(request.CoordenadaX, request.CoordenadaY, request.MotoId);
            await _repository.AddAsync(localizacao);
            return localizacao;
        }

        public async Task<LocalizacaoUWB?> UpdateAsync(Guid id, UpdateLocalizacaoRequest request)
        {
            var localizacao = await _repository.GetByIdAsync(id);
            if (localizacao == null) return null;

            localizacao.AtualizarCoordenadas(request.CoordenadaX, request.CoordenadaY);
            await _repository.UpdateAsync(localizacao);
            return localizacao;
        }

        public async Task<bool> DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);

        public async Task<PaginatedResult<LocalizacaoResponse>> GetPaginatedAsync(int page, int pageSize)
        {
            var (items, totalItems) = await _repository.GetPaginatedAsync(page, pageSize);

            var responseItems = items.Select(l => new LocalizacaoResponse
            {
                Id = l.Id,
                CoordenadaX = l.CoordenadaX,
                CoordenadaY = l.CoordenadaY,
                MotoId = l.MotoId
            }).ToList();

            return new PaginatedResult<LocalizacaoResponse>
            {
                Items = responseItems,
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}
