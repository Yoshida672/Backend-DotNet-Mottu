using Backend_Dotnet_Mottu.Application.DTOs.Request;
using Backend_Dotnet_Mottu.Application.DTOs.Response;
using Backend_Dotnet_Mottu.Domain.Pagination;
using Backend_Dotnet_Mottu.Domain.ValueObjects;

namespace Backend_Dotnet_Mottu.Application.UseCases
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

        public async Task<LocalizacaoUWB?> GetByIdAsync(long id) =>
            await _repository.GetByIdAsync(id);

        public async Task<LocalizacaoUWB> CreateAsync(CreateLocalizacaoUwb request)
        {
            var localizacao = new LocalizacaoUWB(
                request.CoordenadaX,
                request.CoordenadaY
            );

            await _repository.AddAsync(localizacao);
            return localizacao;
        }

        public async Task<LocalizacaoUWB?> UpdateAsync(long id, UpdateLocalizacaoRequest request)
        {
            var localizacao = await _repository.GetByIdAsync(id);
            if (localizacao == null) return null;

            localizacao.AtualizarCoordenadas(request.CoordenadaX, request.CoordenadaY);

        
            await _repository.UpdateAsync(localizacao);
            return localizacao;
        }

        public async Task<bool> DeleteAsync(long id) =>
            await _repository.DeleteAsync(id);

        public async Task<PaginatedResult<LocalizacaoResponse>> GetPaginatedAsync(int page, int pageSize)
        {
            var (items, totalItems) = await _repository.GetPaginatedAsync(page, pageSize);

            var responseItems = items.Select(l => new LocalizacaoResponse
            {
                Id = l.Id,
                CoordenadaX = l.CoordenadaX,
                CoordenadaY = l.CoordenadaY,
                DataHora = l.DataHora
            
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
