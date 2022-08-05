using HotelsBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace HotelsBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        private readonly DataSource _dataSource;

        public HotelsController(DataSource dataSource)
        {
            this._dataSource = dataSource;
        }

        [HttpGet]
        public IActionResult GetAllHotels()
        {
            var hotels = _dataSource.Hotels;
            return Ok(hotels);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetHotelById(int id)
        {
            var hotels = _dataSource.Hotels;
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
            var hotels = _dataSource.Hotels;
            hotels.Add(hotel);
            return CreatedAtAction(nameof(GetHotelById), new {id = hotel.HotelId}, hotel);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateHotel([FromBody] Hotel updated, int id)
        {
            var hotels = _dataSource.Hotels;
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
            var hotels = _dataSource.Hotels;
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
