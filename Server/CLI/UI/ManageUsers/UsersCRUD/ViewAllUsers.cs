using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers.UsersCRUD;

public class ViewAllUsers
{
    private IUserRepository _userRepository;
    private ManageUsers _manageUsers;

    public ViewAllUsers(IUserRepository userRepository, ManageUsers manageUsers)
    {
        _userRepository = userRepository;
        _manageUsers = manageUsers;
    }

    public async Task SeeAllUsers()
    {
        Console.Clear();
        
        ShowUsers();

        Console.WriteLine();
        Console.WriteLine("Press 1 to go back to the menu:");
        string input;

        do
        {
            input = Console.ReadLine();
            if (input.Equals("1"))
            {
               await _manageUsers.ShowMenu();
            }
        } while (!input.Equals("1"));
    }

    public void ShowUsers()
    {
        Console.WriteLine("All users:");
        List<User> users = _userRepository.GetMany().ToList();

        foreach (var user in users)
        {
            Console.WriteLine(user.Id + ") " + user.Username);
        }
    }
}