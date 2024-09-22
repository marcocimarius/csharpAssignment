using Entities;
using FileRepositories;
using RepositoryContracts;

namespace CLI.UI.ManagePosts.PostsCRUD;

public class CreatePost
{
    private ManagePosts _managePosts;
    
    public CreatePost(ManagePosts managePosts)
    {
        _managePosts = managePosts;
    }

    public async Task CreateNewPost()
    {
        Console.WriteLine("Enter post title: ");
        string? title;
        while (true)
        {
            title = Console.ReadLine();
            if (title is not null)
            {
                break;
            }
        }

        Console.WriteLine("Enter post body: ");
        string? body;
        while (true)
        {
            body = Console.ReadLine();
            if (body is not null)
            {
                break;
            }
        }
        
        await TestForNow.AddOneItemAsync(new Post(title, body, 1));
    }
}