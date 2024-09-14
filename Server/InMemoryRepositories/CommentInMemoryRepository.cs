using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private List<Comment> comments;

    public CommentInMemoryRepository()
    {
        comments = new List<Comment>();
        _ = AddAsync(new Comment("Cats are great!", 1, 1)).Result;
        _ = AddAsync(new Comment("So true!", 1, 2)).Result;
        _ = AddAsync(new Comment("They're just so fluffy", 1, 2)).Result;
        _ = AddAsync(new Comment("Mine's hairless!", 1, 1)).Result;
        _ = AddAsync(new Comment("Is it sick?!", 1, 4)).Result;
    }
    
    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any()
            ? comments.Max(c => c.Id) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        Comment? existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (existingComment == null)
        {
            throw new InvalidOperationException($"Comment with id {comment.Id} does not exist");
        }

        comments.Remove(existingComment);
        comments.Add(comment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        Comment? commentToRemove = comments.SingleOrDefault(c => c.Id == id);
        if (commentToRemove == null)
        {
            throw new InvalidOperationException($"Comment with id {id} not found");
        }
        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingle(int id)
    {
        Comment? comment = comments.SingleOrDefault(c => c.Id == id);
        if (comment == null)
        {
            throw new InvalidOperationException($"Comment with id {id} not found");
        }
        return Task.FromResult(comment);
    }

    public IQueryable<Comment> GetMany(int postId)
    {
        return comments.AsQueryable().Where(c => c.PostId == postId);
    }
}