namespace CLI.UI;

public class EntityId
{

    // public EntityId(ManageUsers.ManageUsers manageUsers)
    // {
    //     _manageUsers = manageUsers;
    // }
    
    public async Task<int> ReturnUsersId(string message, ManageUsers.ManageUsers users)
    {
        int id;
        while (true)
        {
            Console.WriteLine(message);
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                await users.ShowMenu();
            }
            else
            {
                if (int.TryParse(key.KeyChar.ToString(), out id))
                {
                    break;   
                }
            }    
        }

        return id;
    }
    
    public async Task<int> ReturnPostsId(string message, ManagePosts.ManagePosts posts)
    {
        int id;
        while (true)
        {
            Console.WriteLine(message);
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                await posts.ShowMenu();
            }
            else
            {
                if (int.TryParse(key.KeyChar.ToString(), out id))
                {
                    break;   
                }
            }    
        }

        return id;
    }
}