using Backend_Dotnet_Mottu.Domain.Entities;

namespace Backend_Dotnet_Mottu.Infrastructure.Persistence;

public interface IUserRepository
{
    User GetUserByEmailAsync(string email);
    Task AddUserAsync(User user);
    Task<List<User>> GetUsersAsync();
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}