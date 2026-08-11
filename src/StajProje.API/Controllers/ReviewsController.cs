using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StajProje.Application.Features.Reviews.Commands.AddReview;

namespace StajProje.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Movie,Admin")]

public class ReviewsController : ControllerBase
{
    private readonly ISender _sender;

    public ReviewsController (ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> AddReview(AddReviewCommand command)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value); // token'dan UserId çıkarma
        command.UserId = userId;

        var reviewId = await _sender.Send(command);
        return Ok(reviewId);
    }
}
