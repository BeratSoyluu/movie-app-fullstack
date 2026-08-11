namespace StajProje.Domain.Entities;

public class Suggestion
{
    public int Id { get; set; }
    public string MovieName { get; set; } = string.Empty;
    public int SuggestedByUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}