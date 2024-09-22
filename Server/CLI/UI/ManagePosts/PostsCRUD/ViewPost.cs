using Entities;
using FileRepositories;
using RepositoryContracts;

namespace CLI.UI.ManagePosts.PostsCRUD;

public class ViewPost
{
    private ManagePosts _managePosts;

    public ViewPost(ManagePosts managePosts)
    {
        _managePosts = managePosts;
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
        List<Post> posts = TestForNow.ReadFromFileAsync<Post>().Result;
        Post post = posts.FirstOrDefault(p => p.Id == id)!;
        
        Console.WriteLine(post.Title);
        Console.WriteLine(post.Body);
    }

    private void ShowPostComments(int id)
    {
        List<Comment> comments = TestForNow.ReadFromFileAsync<Comment>().Result;
        foreach (var comment in comments)
        {
            Console.WriteLine(comment.Id + ") " + comment.Body);
        }
    }
}