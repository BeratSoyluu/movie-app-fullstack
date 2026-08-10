
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StajProje.Application.Features.Movies.Queries.GetPopularMovies;


namespace StajProje.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class MoviesController : ControllerBase
{
    private readonly ISender _sender;

    public MoviesController (ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("popular")]
    public async Task<IActionResult> GetPopularMovies([FromQuery] int page = 1)
    {
        var query = new GetPopularMoviesQuery { Page = page };
        var result = await _sender.Send(query);
        return Ok(result);
    }
}