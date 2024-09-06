using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task GetSingle(int id);
    IQueryable<User> GetMany();
}