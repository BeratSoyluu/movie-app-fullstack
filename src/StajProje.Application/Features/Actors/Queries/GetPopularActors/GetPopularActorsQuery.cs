using MediatR;
using StajProje.Application.Dtos.Tmdb;

namespace StajProje.Application.Features.Actors.Queries.GetPopularActors;
public class GetPopularActorsQuery : IRequest<ActorPageDto>
{
    public int Page { get; set; }
}