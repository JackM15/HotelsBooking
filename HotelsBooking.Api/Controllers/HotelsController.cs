using HotelsBooking.Dal;
using HotelsBooking.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HotelsBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {

        private readonly ILogger _logger;
        private readonly HttpContext _http;
        private readonly DataContext _ctx;

        public HotelsController(ILogger<HotelsController> logger, IHttpContextAccessor httpContextAccesor, DataContext ctx)
        {
            this._logger = logger;
            this._http = httpContextAccesor.HttpContext;
            this._ctx = ctx;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _ctx.Hotels.ToListAsync();
            return Ok(hotels);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
            return Ok(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
        {
            //Add in memory (no db call)
            _ctx.Hotels.Add(hotel);
            //Save all context changes to DB
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.HotelId}, hotel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel updated, int id)
        {
            //Get the hotel
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);

            //Update fields
            hotel.Description = updated.Description;
            hotel.Name = updated.Name;
            hotel.Stars = updated.Stars;

            //Tell db context to update
            _ctx.Hotels.Update(hotel);

            //Call DB to update changes
            await _ctx.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);

            //Remove hotel in memory/context
            _ctx.Hotels.Remove(hotel);

            //Save changes to DB
            await _ctx.SaveChangesAsync();

            return NoContent();
        }

    }
}
