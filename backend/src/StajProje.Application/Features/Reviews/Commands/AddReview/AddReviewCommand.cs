using MediatR;

namespace StajProje.Application.Features.Reviews.Commands.AddReview;

public class AddReviewCommand : IRequest<int>
{
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public int Score { get; set; }
    public string Note { get; set; } = string.Empty;
}