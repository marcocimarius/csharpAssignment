using Entities;
using FileRepositories;
using RepositoryContracts;

namespace CLI.UI.ManageUsers.UsersCRUD;

public class AddUser
{
    private ManageUsers _manageUsers;
    
    public AddUser(ManageUsers manageUsers)
    {
        _manageUsers = manageUsers;
    }

    public async Task CreateUser()
    {
        Console.Clear();
        
        await FileRepository.AddOneItemAsync(new User(NewUserName(), NewPassword()));
        await this._manageUsers.ShowMenu();
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