using CP2_BackEndMottu_DotNet.Application.DTOs.Request;
using CP2_BackEndMottu_DotNet.Domain.Entity;

namespace CP2_BackEndMottu_DotNet.Application.UseCases.impl
{
    public interface ILocalizacaoUseCase
    {
        Task<IEnumerable<LocalizacaoUWB>> GetAllAsync();
        Task<LocalizacaoUWB?> GetByIdAsync(Guid id);
        Task<LocalizacaoUWB> CreateAsync(CreateLocalizacaoUwb localizacao);
        Task<LocalizacaoUWB?> UpdateAsync(Guid id,UpdateLocalizacaoRequest localizacao);
        Task<bool> DeleteAsync(Guid id);

    }

}
