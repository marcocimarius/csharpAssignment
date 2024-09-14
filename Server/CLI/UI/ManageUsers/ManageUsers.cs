using CLI.UI.ManageUsers.UsersCRUD;
using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsers
{
    private IUserRepository _userRepository;
    private CliApp _app;
    public ViewAllUsers ViewAllUsers { get; set; }
    private AddUser AddUser { get; set; }
    private UpdateUser UpdateUser { get; set; }
    private DeleteUser DeleteUser { get; set; }

    public ManageUsers(IUserRepository userRepository, CliApp app)
    {
        _userRepository = userRepository;
        _app = app;
        ViewAllUsers = new ViewAllUsers(_userRepository, this);
        AddUser = new AddUser(_userRepository, this);
        UpdateUser = new UpdateUser(_userRepository, this);
        DeleteUser = new DeleteUser(_userRepository, this);
    }

    public async Task ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("Manage users:");
        Console.WriteLine("1. View all users");
        Console.WriteLine("2. Add new user");
        Console.WriteLine("3. Edit user");
        Console.WriteLine("4. Delete user");
        Console.WriteLine("5. Go back");
        
        string? input = Console.ReadLine();
        bool validation = true;

        do
        {
            switch (input)
            {
                case "1": await ViewAllUsers.SeeAllUsers(); break;
                case "2": await AddUser.CreateUser(); break;
                case "3": await UpdateUser.EditUserMenu(); break;
                case "4": await DeleteUser.DeleteUserMenu(); break;
                case "5": await _app.StartApp(); break;
                default:
                    Console.WriteLine("Invalid input.");
                    validation = false;
                    break;
            }
        } while (validation == false);
    }
}