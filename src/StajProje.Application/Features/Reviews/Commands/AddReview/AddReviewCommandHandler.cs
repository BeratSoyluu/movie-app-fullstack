using MediatR;
using StajProje.Application.Interfaces;
using StajProje.Domain.Entities;

namespace StajProje.Application.Features.Reviews.Commands.AddReview;


public class AddReviewCommandHandler: IRequestHandler<AddReviewCommand,int>
{
    private readonly IReviewRepository _repository;

    public AddReviewCommandHandler(IReviewRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new Review
        {
            MovieId = request.MovieId,
            UserId = request.UserId,
            Score = request.Score,
            Note = request.Note,
            CreatedAt = DateTime.UtcNow
        };
        return await _repository.AddAsync(review);
    }
}
