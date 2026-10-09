using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TheBabyCowboy.HomeApi.Data;

namespace TheBabyCowboy.HomeApi.Controllers
{
    public class TestController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(new
            {
                message = "Test endpoint is working!"
            });
        }
    }
}
