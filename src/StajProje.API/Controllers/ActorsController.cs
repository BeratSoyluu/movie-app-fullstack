using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StajProje.Application.Features.Actors.Queries.GetPopularActors;

namespace StajProje.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Actor,Admin")]

public class ActorsController : ControllerBase
{
    private readonly ISender _sender;

    public ActorsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("popular")]
    public async Task<IActionResult> GetPopularActors([FromQuery] int page = 1)
    {
        var query = new GetPopularActorsQuery {Page = page};
        var result = await _sender.Send(query);
        return Ok(result);
    }
}