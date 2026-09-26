using Microsoft.AspNetCore.Mvc;
using TheBabyCowboy.HomeApi.DTOs;
using TheBabyCowboy.HomeApi.Services;

namespace TheBabyCowboy.HomeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReflectionsController : ControllerBase
{
    private readonly IReflectionService _reflectionService;

    public ReflectionsController(
        IReflectionService reflectionService)
    {
        _reflectionService = reflectionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReflection(
        [FromBody] CreateReflectionDto request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.ThoughtId))
        {
            return BadRequest(new
            {
                message = "ThoughtId is required."
            });
        }

        if (string.IsNullOrWhiteSpace(request.VisitorResponse))
        {
            return BadRequest(new
            {
                message = "VisitorResponse is required."
            });
        }

        try
        {
            var response =
                await _reflectionService.CreateReflectionAsync(
                    request,
                    cancellationToken);

            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new
            {
                message = "Thought not found."
            });
        }
    }
}