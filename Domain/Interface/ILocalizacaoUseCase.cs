using CP2_BackEndMottu_DotNet.Domain.Entity;

namespace CP2_BackEndMottu_DotNet.Domain.Interface
{
    public interface ILocalizacaoUseCase
    {
        Task<IEnumerable<LocalizacaoUWB>> GetAllAsync();
        Task<LocalizacaoUWB?> GetByIdAsync(Guid id);
        Task<LocalizacaoUWB> CreateAsync(Guid motoId, double x, double y);
        Task<LocalizacaoUWB?> UpdateAsync(Guid id, double x, double y);
        Task<bool> DeleteAsync(Guid id);
    }

}
