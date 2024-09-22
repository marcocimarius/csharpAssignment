using Entities;
using FileRepositories;
using RepositoryContracts;

namespace CLI.UI.ManageUsers.UsersCRUD;

public class ViewAllUsers
{
    private ManageUsers _manageUsers;
    
    public ViewAllUsers(ManageUsers manageUsers)
    { 
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
        List<User> users = FileRepository.ReadFromFileAsync<User>().Result;

        foreach (var user in users)
        {
            Console.WriteLine(user.Id + ") " + user.Username);
        }
    }
}