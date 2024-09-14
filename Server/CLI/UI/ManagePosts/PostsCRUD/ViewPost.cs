using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts.PostsCRUD;

public class ViewPost
{
    private IPostRepository _postRepository;
    private ManagePosts _managePosts;
    private ICommentRepository _commentRepository;

    public ViewPost(IPostRepository postRepository, ManagePosts managePosts, ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _managePosts = managePosts;
        _commentRepository = commentRepository;
    }

    public async Task ShowPost(int postId)
    {
        ShowPostInfo(postId);
        
        Console.WriteLine("Comments:");
        ShowPostComments(postId);
        
        await _managePosts.CreateComment.AddComment(postId);

        Console.WriteLine("Write EXIT to go back to the menu: ");
        string? input = Console.ReadLine();
        if (input == "EXIT")
        {
            await _managePosts.ShowMenu();
        }
    }

    private void ShowPostInfo(int id)
    {
        Post post = _postRepository.GetSingle(id).Result;
        Console.WriteLine(post.Title);
        Console.WriteLine(post.Body);
    }

    private void ShowPostComments(int id)
    {
        List<Comment> comments = _commentRepository.GetMany(id).ToList();
        foreach (var comment in comments)
        {
            Console.WriteLine(comment.Id + ") " + comment.Body);
        }
    }
}