using CLI.UI.ManagePosts.PostsCRUD;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePosts
{
    private CliApp _app;
    
    private CreatePost CreatePost { get; set; }
    public ViewPost ViewPost { get; set; }
    private ViewPostsOverview ViewPostsOverview { get; set; }
    public CreateComment CreateComment { get; set; }
    
    public ManagePosts(CliApp app)
    {
        _app = app;
        CreatePost = new CreatePost(this);
        ViewPost = new ViewPost(this);
        ViewPostsOverview = new ViewPostsOverview(this);
        CreateComment = new CreateComment(this);
    }

    public async Task ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("Manage posts:");
        Console.WriteLine("1. View posts overview");
        Console.WriteLine("2. Create post");
        Console.WriteLine("3. Go back");
        
        string? input = Console.ReadLine();
        bool validation = true;

        do
        {
            switch (input)
            {
                case "1": await ViewPostsOverview.ViewPostsOverviewMenu(); break;
                case "2": await CreatePost.CreateNewPost(); break;
                case "3": await _app.StartApp(); break;
                default:
                    Console.WriteLine("Invalid input.");
                    validation = false;
                    break;
            }
        } while (validation == false);
    }
}