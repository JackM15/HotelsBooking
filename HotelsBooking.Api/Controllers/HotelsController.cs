using HotelsBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace HotelsBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {

        private readonly ILogger _logger;

        public HotelsController(ILogger<HotelsController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllHotels()
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetHotelById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateHotel([FromBody] Hotel updated, int id)
        {
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            return NoContent();
        }

    }
}
