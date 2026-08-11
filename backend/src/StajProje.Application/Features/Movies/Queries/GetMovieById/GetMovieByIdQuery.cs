using MediatR;
using StajProje.Application.Dtos;

namespace StajProje.Application.Features.Movies.Queries.GetMovieById;

public class GetMovieByIdQuery : IRequest<MovieDetailDto>
{
    public int MovieId { get; set; }
    public int UserId { get; set; }
}