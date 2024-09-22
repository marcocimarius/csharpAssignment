using Entities;
using FileRepositories;
using RepositoryContracts;

namespace CLI.UI.ManageUsers.UsersCRUD;

public class UpdateUser
{
    // private IUserRepository _userRepository;
    private ManageUsers _manageUsers;

    // public UpdateUser(IUserRepository userRepository, ManageUsers manageUsers)
    // {
    //     _userRepository = userRepository;
    //     _manageUsers = manageUsers;
    // }
    
    public UpdateUser(ManageUsers manageUsers)
    {
        // _userRepository = userRepository;
        _manageUsers = manageUsers;
    }

    public async Task EditUserMenu()
    {
        Console.Clear();
        _manageUsers.ViewAllUsers.ShowUsers();
        
        await this.EditUser(new EntityId().ReturnUsersId("Choose which user to update or press ESC to go back: ", _manageUsers).Result);
    }

    private async Task EditUser(int id)
    {
        Console.Clear();
        List<User> users = TestForNow.ReadFromFileAsync<User>().Result;
        User user = users.SingleOrDefault(u => u.Id == id)!;
        Console.WriteLine("Editing user " + user.Username);
        
        await TestForNow.RemoveOneItemAsync(user);
        await TestForNow.AddOneItemAsync(new User(NewUserName(), NewPassword()));
        await EditUserMenu();
    }

    private string NewUserName()
    {
        Console.WriteLine("Write the name of the new user: ");
        string? name = Console.ReadLine();
        do
        {
            Console.WriteLine("You have to input a name: ");
            name = Console.ReadLine();
        } while (name is null);

        return name;
    }

    private string NewPassword()
    {
        Console.WriteLine("Enter the password: ");
        string? password = Console.ReadLine();
        do
        {
            Console.WriteLine("You have to input a password: ");
            password = Console.ReadLine();
        } while (password is null);

        return password;
    }
}