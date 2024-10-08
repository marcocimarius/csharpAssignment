using DTOs.Posts;
using Entities;
using FileRepositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreatePost([FromBody] CreatePostDto request)
    {
        Post post = new Post(request.Title, request.Body, request.UserId);
        Post created = await FileRepository.AddOneItemAsync(post);
        return Created($"Posts/{created.Id}", created);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetPost([FromRoute] int id)
    {
        Post? post = await FileRepository.ReadOneFromFileAsync<Post>(id);

        if (post == null)
        {
            return NotFound($"Post with id {id} not found");
        }
        
        return Ok(post);
    }

    [HttpGet("all")]
    public async Task<IResult> GetAllPosts()
    {
        List<Post> posts = await FileRepository.ReadFromFileAsync<Post>();

        if (posts.Count == 0)
        {
            return Results.NotFound("No posts found");
        }
        
        return Results.Ok(posts);
    }

    [HttpGet("title/userId/")]
    public async Task<IResult> GetPostByUserIdWithSpecificInputInTitle([FromQuery] string request,
        [FromQuery] int userId)
    {
        List<Post> allPosts = await FileRepository.ReadFromFileAsync<Post>();
        if (allPosts.Count == 0)
        {
            return Results.NotFound("No posts found");
        }
        
        List<Post> postsByUserId = allPosts.Where(p => p.UserId == userId).ToList();
        if (postsByUserId.Count == 0)
        {
            return Results.NotFound($"No posts found with id {userId}");
        }
        
        List<Post> neededPosts = postsByUserId.Where(p => p.Title.Contains(request)).ToList();
        if (neededPosts.Count == 0)
        {
            return Results.NotFound($"No posts found with {request} in title");
        }
        
        return Results.Ok(neededPosts);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdatePost([FromRoute] int id, [FromBody] CreatePostDto request)
    {
        Post? post = await FileRepository.ReadOneFromFileAsync<Post>(id);
        
        if (post == null)
        {
            return NotFound($"Post with id {id} not found");
        }

        await FileRepository.RemoveOneItemAsync<Post>(id);
        post.Title = request.Title;
        post.Body = request.Body;
        post.UserId = request.UserId;
        
        return Ok(await FileRepository.AddOneItemAsync(post));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePost([FromRoute] int id)
    {
        await FileRepository.RemoveOneItemAsync<Post>(id);

        return NoContent();
    }
}