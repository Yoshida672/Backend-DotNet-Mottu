using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Infrastructure.Persistence.Repositories;

namespace CP2_BackEndMottu_DotNet.Application.UseCases
{
    public class LocalizacaoUseCase
    {
        private readonly IRepository<LocalizacaoUWB> _repository;

        public LocalizacaoUseCase(IRepository<LocalizacaoUWB> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LocalizacaoResponse>> GetAllAsync()
        {
            var localizacoes = await _repository.GetAllAsync();
            return localizacoes.Select(x => new LocalizacaoResponse
            {
                Id = x.Id,
                CoordenadaX = x.CoordenadaX,
                CoordenadaY = x.CoordenadaY,
                DataHora = x.DataHora,
                MotoId = x.MotoId
            });
        }

        public async Task<LocalizacaoResponse?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            return new LocalizacaoResponse
            {
                Id = entity.Id,
                CoordenadaX = entity.CoordenadaX,
                CoordenadaY = entity.CoordenadaY,
                DataHora = entity.DataHora,
                MotoId = entity.MotoId
            };
        }

        public async Task<LocalizacaoResponse> CreateAsync(CreateLocalizacaoUwb request)
        {
            var nova = new LocalizacaoUWB(request.CoordenadaX, request.CoordenadaY, request.MotoId);
            await _repository.AddAsync(nova);

            return new LocalizacaoResponse
            {
                Id = nova.Id,
                CoordenadaX = nova.CoordenadaX,
                CoordenadaY = nova.CoordenadaY,
                DataHora = nova.DataHora,
                MotoId = nova.MotoId
            };
        }

        public async Task<LocalizacaoUWB?> UpdateAsync(Guid id, UpdateLocalizacaoRequest request)
        {
            var localizacao = await _repository.GetByIdAsync(id);
            if (localizacao == null)
                return null;
            localizacao.AtualizarCoordenadas(request.CoordenadaX, request.CoordenadaY);
            localizacao.AtualizarMotoId( request.MotoId);

            await _repository.UpdateAsync(localizacao);
            return localizacao;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var localizacao = await _repository.GetByIdAsync(id);
            if (localizacao == null)
                return false;

            await _repository.DeleteAsync(localizacao);
            return true;
        }
    }
}
