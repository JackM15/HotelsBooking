using HotelsBooking.Api.Services;
using HotelsBooking.Api.Services.Abstractions;
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
        private readonly MyFirstService _firstService;
        private readonly ISingletonOperation _singleton;
        private readonly ITransientOperation _transient;
        private readonly IScopedOperation _scoped;
        private readonly ILogger _logger;

        public HotelsController(MyFirstService firstService, 
            ISingletonOperation singleton, 
            ITransientOperation transient, 
            IScopedOperation scoped,
            ILogger<HotelsController> logger)
        {
            this._firstService = firstService;
            this._singleton = singleton;
            this._transient = transient;
            this._scoped = scoped;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllHotels()
        {
            //Log GUIDs for each service time
            _logger.LogInformation($"GUID of singleton: {_singleton.Guid}");
            _logger.LogInformation($"GUID of transient: {_transient.Guid}");
            _logger.LogInformation($"GUID of scoped: {_scoped.Guid}");

            var hotels = _firstService.GetHotels();
            return Ok(hotels);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetHotelById(int id)
        {
            var hotels = _firstService.GetHotels();
            var hotel = hotels.FirstOrDefault(hotel => hotel.HotelId == id);

            if (hotel == null)
            {
                return NotFound("No resource with the corresponding ID found.");
            }

            return Ok(hotel);
        }

        [HttpPost]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            var hotels = _firstService.GetHotels();
            hotels.Add(hotel);
            return CreatedAtAction(nameof(GetHotelById), new {id = hotel.HotelId}, hotel);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateHotel([FromBody] Hotel updated, int id)
        {
            var hotels = _firstService.GetHotels();
            var old = hotels.FirstOrDefault(h => h.HotelId == id);

            if (old == null)
            {
                return NotFound("No resource with the corresponding ID found.");
            }
            hotels.Remove(old);
            hotels.Add(updated);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            var hotels = _firstService.GetHotels();
            var toDelete = hotels.FirstOrDefault(h => h.HotelId == id);

            if (toDelete == null)
            {
                return NotFound("No resource with the corresponding ID found.");
            }

            hotels.Remove(toDelete);

            return NoContent();
        }

    }
}
