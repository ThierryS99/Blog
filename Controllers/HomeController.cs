using Microsoft.AspNetCore.Mvc;

// Health check
namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public ActionResult Get([FromServices] IConfiguration config)
        {
            var env = config.GetValue<string>("Env");
            return Ok(new
            {
                enviroment = env
            });
        }
    }
}
