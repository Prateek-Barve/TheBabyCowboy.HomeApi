using Microsoft.AspNetCore.Mvc;
using TheBabyCowboy.HomeApi.DTOs;
using TheBabyCowboy.HomeApi.Services;

namespace TheBabyCowboy.HomeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ThoughtsController : ControllerBase
{
    private readonly IThoughtService _thoughtService;

    public ThoughtsController(IThoughtService thoughtService)
    {
        _thoughtService = thoughtService;
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomThought(
        CancellationToken cancellationToken)
    {
        var thought = await _thoughtService
            .GetRandomThoughtAsync(cancellationToken);

        if (thought is null)
        {
            return NotFound(new
            {
                message = "No thoughts are available."
            });
        }

        var response = new ThoughtResponseDto
        {
            Id = thought.Id,
            Text = thought.Text
        };

        return Ok(response);
    }
}