
using CP2_BackEndMottu_DotNet.Domain.Entity;
using CP2_BackEndMottu_DotNet.Domain.Interface;

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

        public async Task<LocalizacaoUWB> CreateAsync(Guid motoId, double x, double y)
        {
            var localizacao = new LocalizacaoUWB(x, y, motoId);
            await _repository.AddAsync(localizacao);
            return localizacao;
        }

        public async Task<LocalizacaoUWB?> UpdateAsync(Guid id, double x, double y)
        {
            var localizacao = await _repository.GetByIdAsync(id);
            if (localizacao == null) return null;

            localizacao.AtualizarCoordenadas(x, y);
            await _repository.UpdateAsync(localizacao);
            return localizacao;
        }

        public async Task<bool> DeleteAsync(Guid id) =>
            await _repository.DeleteAsync(id);
    }

}
