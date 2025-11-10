using Backend_Dotnet_Mottu.Application.DTOs.Request;
using Backend_Dotnet_Mottu.Domain.ValueObjects;

namespace Backend_Dotnet_Mottu.Application.UseCases
{
    public interface ILocalizacaoUseCase
    {
        Task<IEnumerable<LocalizacaoUWB>> GetAllAsync();
        Task<LocalizacaoUWB?> GetByIdAsync(long id);
        Task<LocalizacaoUWB> CreateAsync(CreateLocalizacaoUwb localizacao);
        Task<LocalizacaoUWB?> UpdateAsync(long id, UpdateLocalizacaoRequest localizacao);
        Task<bool> DeleteAsync(long id);

    }

}
