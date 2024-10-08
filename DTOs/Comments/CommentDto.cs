namespace DTOs.Comments;

public class CommentDto
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public string Body { get; set; }
}