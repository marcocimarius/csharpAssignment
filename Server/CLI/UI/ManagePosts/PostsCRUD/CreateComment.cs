using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts.PostsCRUD;

public class CreateComment
{
    private IPostRepository _postRepository;
    private ManagePosts _managePosts;
    private ICommentRepository _commentRepository;

    public CreateComment(IPostRepository postRepository, ManagePosts managePosts, ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _managePosts = managePosts;
        _commentRepository = commentRepository;
    }

    public async Task AddComment(int postId)
    {
        Console.WriteLine("Add a new comment (EXIT to stop): ");
        string? input;

        while (true)
        {
            input = Console.ReadLine();
            if (input is not null)
            {
                if (!input.Equals("EXIT"))
                {
                    await _commentRepository.AddAsync(new Comment(input, postId, 1));
                }
                else
                {
                    break;  
                }
            }
        }
    }
}