using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagePosts.PostsCRUD;

public class ViewPostsOverview
{
    private IPostRepository _postRepository;
    private ManagePosts _managePosts;

    public ViewPostsOverview(IPostRepository postRepository, ManagePosts managePosts)
    {
        _postRepository = postRepository;
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
        List<Post> posts = _postRepository.GetMany().ToList();

        foreach (var post in posts)
        {
            Console.WriteLine(post.Id + ") " + post.Title);
        }
    }
}