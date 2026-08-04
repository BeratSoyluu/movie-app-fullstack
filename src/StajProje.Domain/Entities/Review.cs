namespace StajProje.Domain.Entities;

public class Review
{
    public int Id{ get; set; }
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public int Score { get; set; }
    public string Note { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

}