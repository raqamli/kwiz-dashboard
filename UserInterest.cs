public class UserInterest
{
    public Guid UserId { get; set; }
    public IEnumerable<string> Interests { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}