namespace CP2_BackEndMottu_DotNet.Domain.Interface
{
    public interface IUseCase<Entity, Create, Update, Response>
    {
        Task<IEnumerable<Response>> GetAllAsync();
        Task<Response?> GetByIdAsync(Guid id);
        Task<Response> CreateAsync(Create request);
        Task<Response?> UpdateAsync(Guid id, Update request);
        Task<bool> DeleteAsync(Guid id);
    }
}
