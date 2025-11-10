using Backend_Dotnet_Mottu.Domain.Pagination;

namespace Backend_Dotnet_Mottu.Application.UseCases
{
    public interface IUseCase<Entity, Create, Update, Response>
    {
        Task<IEnumerable<Response>> GetAllAsync();
        Task<Response?> GetByIdAsync(long id);
        Task<Response> CreateAsync(Create request);
        Task<Response?> UpdateAsync(long id, Update request);
        Task<bool> DeleteAsync(long id);
        Task<PaginatedResult<Response>> GetPaginatedAsync(int page, int pageSize);

    }
}
