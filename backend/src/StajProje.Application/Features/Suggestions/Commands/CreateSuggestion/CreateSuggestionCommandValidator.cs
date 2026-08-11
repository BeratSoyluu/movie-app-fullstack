using FluentValidation;
using StajProje.Application.Features.Suggestions.Commands.CreateSuggestion;

namespace StajProje.Application.Features.Suggestions.Commands.CreateSuggestion;
public class CreateSuggestionCommandValidator : AbstractValidator<CreateSuggestionCommand>
{
    public CreateSuggestionCommandValidator()
    {
        RuleFor(x => x.MovieName)
            .NotEmpty().WithMessage("Film adı boş olamaz.")
            .MaximumLength(200).WithMessage("Film adı en fazla 200 karakter olabilir.");
    }
}