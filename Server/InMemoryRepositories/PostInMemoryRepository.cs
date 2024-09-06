using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private List<Post> posts;

    public PostInMemoryRepository()
    {
        posts = new List<Post>();
    }
    
    public Task AddAsync(Post post)
    {
        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1
            : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);
        if (existingPost == null)
        {
            throw new InvalidOperationException($"Post with id {post.Id} does not exist");
        }

        posts.Remove(existingPost);
        posts.Add(post);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id);
        if (postToRemove == null)
        {
            throw new InvalidOperationException($"Post with id {id} not found");
        }
        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public Task GetSingle(int id)
    {
        Post? post = posts.SingleOrDefault(p => p.Id == id);
        if (post == null)
        {
            throw new InvalidOperationException($"Post with id {id} not found");
        }
        return Task.FromResult(post);
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }
}