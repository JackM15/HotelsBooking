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
        public HotelsController()
        { }

        [HttpGet]
        public IActionResult GetAllHotels()
        {
            var hotels = GetHotels();
            return Ok(hotels);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetHotelById(int id)
        {
            var hotels = GetHotels();
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
            var hotels = GetHotels();
            hotels.Add(hotel);
            return CreatedAtAction(nameof(GetHotelById), new {id = hotel.HotelId}, hotel);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateHotel([FromBody] Hotel updated, int id)
        {
            var hotels = GetHotels();
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
            var hotels = GetHotels();
            var toDelete = hotels.FirstOrDefault(h => h.HotelId == id);

            if (toDelete == null)
            {
                return NotFound("No resource with the corresponding ID found.");
            }

            hotels.Remove(toDelete);

            return NoContent();
        }

        // Temp Data
        private List<Hotel> GetHotels()
        {
            return new List<Hotel>
            {
                new Hotel
                {
                    HotelId = 1,
                    Name = "Nango Hotel",
                    Stars = 3,
                    Address = "1 Nango Road",
                    City = "Maidstone",
                    Country = "UK",
                    Description = "The Nango Hotel, a place to chill with kibble on demand.",
                },
                new Hotel
                {
                    HotelId = 2,
                    Name = "Yeggle Hotel",
                    Stars = 1,
                    Address = "1 Eggle Road",
                    City = "Maidstone",
                    Country = "UK",
                    Description = "The Yeggle Hotel, Next to a train.",
                }
            };
        }
    }
}
