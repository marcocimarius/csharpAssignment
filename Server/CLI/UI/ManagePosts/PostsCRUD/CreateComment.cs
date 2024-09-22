using Entities;
using FileRepositories;
using RepositoryContracts;

namespace CLI.UI.ManagePosts.PostsCRUD;

public class CreateComment
{
    private ManagePosts _managePosts;

    public CreateComment(ManagePosts managePosts)
    {
        _managePosts = managePosts;
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
                    await FileRepository.AddOneItemAsync(new Comment(input, postId, 1));
                }
                else
                {
                    break;  
                }
            }
        }
    }
}