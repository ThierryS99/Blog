using Microsoft.AspNetCore.Mvc;

// Health check
namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
