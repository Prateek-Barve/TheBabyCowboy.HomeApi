using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TheBabyCowboy.HomeApi.Data;

namespace TheBabyCowboy.HomeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(new
            {
                message = "Test endpoint is working (once again edited for pipeline auto run test)!"
            });
        }
    }
}