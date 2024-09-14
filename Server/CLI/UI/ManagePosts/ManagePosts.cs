using CLI.UI.ManagePosts.PostsCRUD;
using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePosts
{
    private IPostRepository _postRepository;
    private ICommentRepository _commentRepository;
    private CliApp _app;
    
    private CreatePost CreatePost { get; set; }
    public ViewPost ViewPost { get; set; }
    private ViewPostsOverview ViewPostsOverview { get; set; }
    public CreateComment CreateComment { get; set; }

    public ManagePosts(IPostRepository postRepository, CliApp app, ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _app = app;
        _commentRepository = commentRepository;
        CreatePost = new CreatePost(_postRepository, this);
        ViewPost = new ViewPost(_postRepository, this, _commentRepository);
        ViewPostsOverview = new ViewPostsOverview(_postRepository, this);
        CreateComment = new CreateComment(_postRepository, this, _commentRepository);
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