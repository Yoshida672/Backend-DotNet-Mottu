using MongoDB.Driver;
using Backend_Dotnet_Mottu.Domain.Entities;
using Backend_Dotnet_Mottu.Infrastructure;
using Backend_Dotnet_Mottu.Infrastructure.Persistence;

namespace Backend_Dotnet_Mottu.Infrasctructure.Persistence.Repositories;

public class UserRepository(MongoContext mongoContext) : IUserRepository
{
    private readonly IMongoCollection<User> _users = mongoContext.Database.GetCollection<User>("UsersPM");

    public User GetUserByEmailAsync(string email)
    {
        return _users.Find(x => x.Email == email).FirstOrDefault();
    }

    public async Task AddUserAsync(User user)
    {
        await _users.InsertOneAsync(user);
    }

    public Task<List<User>> GetUsersAsync()
    {
       return _users.Find(x => true).ToListAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
      await _users.ReplaceOneAsync(x => x.Email == user.Email, user);
    }

    public async Task DeleteUserAsync(User user)
    {
        await _users.DeleteOneAsync(x => x.Email == user.Email);
    }
}