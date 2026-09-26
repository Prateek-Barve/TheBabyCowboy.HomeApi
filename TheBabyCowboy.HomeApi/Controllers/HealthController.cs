using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TheBabyCowboy.HomeApi.Data;

namespace TheBabyCowboy.HomeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly MongoDbContext _mongoDbContext;

    public HealthController(MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var result = _mongoDbContext.Database
                .RunCommand<BsonDocument>(
                    new BsonDocument("ping", 1)
                );

            return Ok(new
            {
                status = "healthy",
                database = "connected"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                status = "unhealthy",
                database = "disconnected",
                error = ex.Message
            });
        }
    }
}