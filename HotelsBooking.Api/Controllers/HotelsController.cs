using AutoMapper;
using HotelsBooking.Api.Dtos;
using HotelsBooking.Dal;
using HotelsBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelsBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {

        private readonly ILogger _logger;
        private readonly DataContext _ctx;
        private readonly IMapper _mapper;

        public HotelsController(ILogger<HotelsController> logger, DataContext ctx, IMapper mapper)
        {
            this._logger = logger;
            this._ctx = ctx;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _ctx.Hotels.ToListAsync();
            //Map the hotels to the HotelGetDto
            var hotelsGet = _mapper.Map<List<HotelGetDto>>(hotels);
            return Ok(hotelsGet);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
            //Map the hotel to a hotelgetdto
            var hotelGet = _mapper.Map<HotelGetDto>(hotel);
            return Ok(hotelGet);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDto hotel)
        {
            //Map the hotelcreatedto to a hotel _mapper.Map<Destination>(tomap)
            var domainHotel = _mapper.Map<Hotel>(hotel);

            //Add in memory (no db call)
            _ctx.Hotels.Add(domainHotel);
            //Save all context changes to DB
            await _ctx.SaveChangesAsync();

            //Map the domain hotel to a hotelgetdo
            var hotelGet = _mapper.Map<HotelGetDto>(domainHotel);

            return CreatedAtAction(nameof(GetHotelById), new { id = domainHotel.HotelId}, hotelGet);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelCreateDto updated, int id)
        {
            //Map the updated object to a hotel
            var toUpdate = _mapper.Map<Hotel>(updated);
            toUpdate.HotelId = id;

            //Tell db context to update the data
            _ctx.Hotels.Update(toUpdate);

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
