using Entities;

namespace RepositoryContracts;

public interface IPostRepository
{
    Task AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(int id);
    Task GetSingle(int id);
    IQueryable<Post> GetMany();
}