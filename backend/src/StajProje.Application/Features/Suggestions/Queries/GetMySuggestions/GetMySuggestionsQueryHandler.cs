using MediatR;
using StajProje.Application.Interfaces;
using StajProje.Domain.Entities;

namespace StajProje.Application.Features.Suggestions.Queries.GetMySuggestions;

public class GetMySuggestionsQueryHandler : IRequestHandler<GetMySuggestionsQuery, List<Suggestion>>
{
    private readonly ISuggestionRepository _suggestionRepository;

    public GetMySuggestionsQueryHandler(ISuggestionRepository suggestionRepository)
    {
        _suggestionRepository = suggestionRepository;
    }

    public async Task<List<Suggestion>> Handle(GetMySuggestionsQuery request, CancellationToken cancellationToken)
    {
        return await _suggestionRepository.GetByUserIdAsync(request.UserId);
    }
}