using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private IUserRepository _userRepository;
    private ICommentRepository _commentRepository;
    private IPostRepository _postRepository;
    
    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        this._userRepository = userRepository;
        this._commentRepository = commentRepository;
        this._postRepository = postRepository;
    }

    public async Task StartApp()
    {
        Console.Clear();
        Console.WriteLine("Choose an option:");
        Console.WriteLine("1) Manage users");
        Console.WriteLine("2) Manage posts");
        Console.WriteLine("3) Exit");
        
        string? input = Console.ReadLine();
        bool validation = true;
        
        do
        {
            switch (input)
            {
                case "1": await new ManageUsers.ManageUsers(_userRepository, this).ShowMenu(); break; //call users
                case "2": await new ManagePosts.ManagePosts(_postRepository, this, _commentRepository).ShowMenu(); break; //call posts
                case "3": Console.WriteLine("Exiting app"); Environment.Exit(0); break; //exit
                default: Console.WriteLine("Invalid input");
                    validation = false; 
                    break; //bad input
            }    
        } while(validation == false);
    }
}