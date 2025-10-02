using CP2_BackEndMottu_DotNet.Application.DTOs.Response;
using CP2_BackEndMottu_DotNet.Domain.Pagination;

namespace CP2_BackEndMottu_DotNet.Application.UseCases.impl
{
    public interface IUseCase<Entity, Create, Update, Response>
    {
        Task<IEnumerable<Response>> GetAllAsync();
        Task<Response?> GetByIdAsync(Guid id);
        Task<Response> CreateAsync(Create request);
        Task<Response?> UpdateAsync(Guid id, Update request);
        Task<bool> DeleteAsync(Guid id);
        Task<PaginatedResult<Response>> GetPaginatedAsync(int page, int pageSize);

    }
}
