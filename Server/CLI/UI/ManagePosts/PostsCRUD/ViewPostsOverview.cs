using Entities;
using FileRepositories;
using RepositoryContracts;

namespace CLI.UI.ManagePosts.PostsCRUD;

public class ViewPostsOverview
{
    private ManagePosts _managePosts;

    public ViewPostsOverview(ManagePosts managePosts)
    {
        _managePosts = managePosts;
    }

    public async Task ViewPostsOverviewMenu()
    {
        Console.Clear();
        ShowAllPostsOverview();
        
        await _managePosts.ViewPost.ShowPost(await new EntityId().ReturnPostsId("Choose which post to see in detail: ", _managePosts));
    }

    private void ShowAllPostsOverview()
    {
        List<Post> posts = TestForNow.ReadFromFileAsync<Post>().Result;

        foreach (var post in posts)
        {
            Console.WriteLine(post.Id + ") " + post.Title);
        }
    }
}