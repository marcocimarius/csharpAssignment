namespace EfcRepository;

public class EfcComment
{
    public int Id { get; set; }
    public string Body { get; set; } = null!;
    
    public int UserId { get; set; }
    public EfcUser EfcUser { get; set; } = null!;
    
    public int PostId { get; set; }
    public EfcPost EfcPost { get; set; } = null!;
}