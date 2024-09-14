using RepositoryContracts;

namespace CLI.UI.ManageUsers.UsersCRUD;

public class DeleteUser
{
    private IUserRepository _userRepository;
    private ManageUsers _manageUsers;

    public DeleteUser(IUserRepository userRepository, ManageUsers manageUsers)
    {
       _userRepository = userRepository;
       _manageUsers = manageUsers;
    }


    public async Task DeleteUserMenu()
    {
        Console.Clear();
        _manageUsers.ViewAllUsers.ShowUsers();

        await RemoveUser(new EntityId().ReturnUsersId("Choose which user to delete or press ESC to go back: ", _manageUsers).Result);
    }

    private async Task RemoveUser(int id)
    {
        Console.WriteLine("Are you sure you want to delete this user: " + _userRepository.GetSingle(id).Result.Username + "? (Y/N)");

        string? input;
        do
        {
            input = Console.ReadLine()?.Trim().ToUpper();
        } while (input != "Y" && input != "N");

        if (input.ToUpper().Equals("Y"))
        {
            await _userRepository.DeleteAsync(id);
        }

        await DeleteUserMenu();
    }
}