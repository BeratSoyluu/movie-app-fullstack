using FluentValidation;
using StajProje.Application.Features.Reviews.Commands.AddReview;

namespace StajProje.Application.Features.Reviews.Commands.AddReview;

public class AddReviewCommandValidator : AbstractValidator<AddReviewCommand>
{
    public AddReviewCommandValidator()
    {
        RuleFor(x => x.Score)
            .InclusiveBetween(1 , 10)
            .WithMessage("Puan 1 ile 10 arasında olmalıdır.");

        RuleFor(x => x.MovieId)
            .GreaterThan(0)
            .WithMessage("Geçerli bir film seçilmelidir.");

        RuleFor(x => x.Note)
            .MaximumLength(500)
            .WithMessage("Not en fazla 500 karakter olabilir.");
    }
}