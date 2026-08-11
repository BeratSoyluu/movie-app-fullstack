using MediatR;
using StajProje.Application.Dtos.Tmdb;
using StajProje.Application.Interfaces;

namespace StajProje.Application.Features.Actors.Queries.SearchActors;

public class SearchActorsQueryHandler : IRequestHandler<SearchActorsQuery, ActorPageDto>
{
    private readonly ITmdbService _tmdbService;

    public SearchActorsQueryHandler(ITmdbService tmdbService)
    {
        _tmdbService = tmdbService;
    }

    public async Task<ActorPageDto> Handle(SearchActorsQuery request, CancellationToken cancellationToken)
{
    return await _tmdbService.SearchActorsAsync(request.Query);
}
}