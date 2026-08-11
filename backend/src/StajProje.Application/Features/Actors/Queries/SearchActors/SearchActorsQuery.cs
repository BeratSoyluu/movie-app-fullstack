using MediatR;
using StajProje.Application.Dtos.Tmdb;

namespace StajProje.Application.Features.Actors.Queries.SearchActors;

public class SearchActorsQuery : IRequest<ActorPageDto>
{
    public string Query { get; set; } = string.Empty;
}