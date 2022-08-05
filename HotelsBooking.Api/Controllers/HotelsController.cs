using Microsoft.AspNetCore.Mvc;

namespace HotelsBooking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelsController : Controller
    {
        public HotelsController()
        { }

        [HttpGet]
        public IActionResult GetRooms()
        {
            return Ok("Hello From The Hotels Controller!");
        }
    }
}
