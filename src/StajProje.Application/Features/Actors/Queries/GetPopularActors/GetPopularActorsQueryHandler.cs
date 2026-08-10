using MediatR;
using StajProje.Application.Dtos.Tmdb;
using StajProje.Application.Interfaces;

namespace StajProje.Application.Features.Actors.Queries.GetPopularActors;

public class GetPopularActorsQueryHandler : IRequestHandler<GetPopularActorsQuery, ActorPageDto>
{
    private readonly ITmdbService _tmdbService;

    public GetPopularActorsQueryHandler(ITmdbService tmdbService)
    {
        _tmdbService = tmdbService;
    }

    public async Task<ActorPageDto> Handle(GetPopularActorsQuery request, CancellationToken cancellationToken)
    {
        return await _tmdbService.GetPopularActorsAsync(request.Page);
    }
}