using MediatR;
using StajProje.Domain.Entities;

namespace StajProje.Application.Features.Suggestions.Queries.GetMySuggestions;

public class GetMySuggestionsQuery : IRequest<List<Suggestion>>
{
    public int UserId { get; set; }
}