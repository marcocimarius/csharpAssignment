using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers.UsersCRUD;

public class UpdateUser
{
    private IUserRepository _userRepository;
    private ManageUsers _manageUsers;

    public UpdateUser(IUserRepository userRepository, ManageUsers manageUsers)
    {
        _userRepository = userRepository;
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
        Console.WriteLine("Editing user " + this._userRepository.GetSingle(id).Result.Username);
        
        await this._userRepository.UpdateAsync(new User(NewUserName(), NewPassword()));
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