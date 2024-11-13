namespace EfcRepository;

public class EfcPost
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
    
    public int UserId { get; set; }
    public EfcUser EfcUser { get; set; } = null!;
}