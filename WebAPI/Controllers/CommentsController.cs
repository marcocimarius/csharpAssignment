using DTOs.Comments;
using Entities;
using FileRepositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> CreateComment([FromBody] CommentDto request)
    {
        Comment comment = new Comment(request.Body, request.PostId, request.UserId);
        Comment created = await FileRepository.AddOneItemAsync(comment);
        return Created($"Comments/{created.Id}", created);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetComment([FromRoute] int id)
    {
        Comment? comment = await FileRepository.ReadOneFromFileAsync<Comment>(id);

        if (comment == null)
        {
            return NotFound($"Comment with id {id} not found");
        }
        
        return Ok(comment);
    }

    [HttpGet("all")]
    public async Task<IResult> GetAllComments()
    {
        List<Comment> comments = await FileRepository.ReadFromFileAsync<Comment>();

        if (comments.Count == 0)
        {
            return Results.NotFound("No comments found");
        }
        
        return Results.Ok(comments);
    }

    [HttpGet("userId/")]
    public async Task<IResult> GetCommentsByUserId([FromQuery] int userId)
    {
        List<Comment> allComments = await FileRepository.ReadFromFileAsync<Comment>();
        if (allComments.Count == 0)
        {
            return Results.NotFound("No comments found");
        }
        
        List<Comment> commentsByUserId = allComments.Where(c => c.UserId == userId).ToList();
        if (commentsByUserId.Count == 0)
        {
            return Results.NotFound($"No comments found with id {userId}");
        }
        
        return Results.Ok(commentsByUserId);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateComment([FromRoute] int id, [FromBody] CommentDto request)
    {
        Comment? comment = await FileRepository.ReadOneFromFileAsync<Comment>(id);
        
        if (comment == null)
        {
            return NotFound($"Comment with id {id} not found");
        }

        await FileRepository.RemoveOneItemAsync<Comment>(id);
        comment.PostId = request.PostId;
        comment.Body = request.Body;
        comment.UserId = request.UserId;
        
        return Ok(await FileRepository.AddOneItemAsync(comment));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteComment([FromRoute] int id)
    {
        await FileRepository.RemoveOneItemAsync<Comment>(id);

        return NoContent();
    }
}