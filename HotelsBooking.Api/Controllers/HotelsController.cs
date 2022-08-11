using AutoMapper;
using HotelsBooking.Api.Dtos;
using HotelsBooking.Dal;
using HotelsBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelsBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {

        private readonly DataContext _ctx;
        private readonly IMapper _mapper;

        public HotelsController(DataContext ctx, IMapper mapper)
        {
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

            if(hotel == null)
            {
                return NotFound();
            }

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

            if (hotel == null)
            {
                return NotFound();
            }

            //Remove hotel in memory/context
            _ctx.Hotels.Remove(hotel);

            //Save changes to DB
            await _ctx.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        [Route("{hotelId}/rooms")]
        public async Task<IActionResult> GetAllHotelRooms(int hotelId)
        {
            //Get all rooms with the matching hotel ID
            var rooms = await _ctx.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();
            //Map them to a getdto
            var mappedRooms = _mapper.Map<List<RoomGetDto>>(rooms);
            return Ok(mappedRooms);
        }

        [HttpGet]
        [Route("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> GetHotelRoomById(int hotelId, int roomId)
        {
            var room = await _ctx.Rooms.FirstOrDefaultAsync(r =>  r.HotelId == hotelId &&  r.RoomId == roomId);
            if (room == null)
            {
                return NotFound("Room not found.");
            }

            var mappedRoom = _mapper.Map<RoomGetDto>(room);

            return Ok(mappedRoom);
        }

        [HttpPost]
        [Route("{hotelId}/rooms")]
        public async Task<IActionResult> AddHotelRoom([FromBody] RoomPostPutDto newRoom, int hotelId)
        {
            var room = _mapper.Map<Room>(newRoom);
            //Get the hotel using the id and include the rooms.
            var hotel = await _ctx.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.HotelId == hotelId);
            //Add the new room to the rooms
            hotel.Rooms.Add(room);
            //Save to db
            await _ctx.SaveChangesAsync();

            var mappedRoom = _mapper.Map<RoomGetDto>(room);

            return CreatedAtAction(nameof(GetHotelRoomById),
                new { hotelId = hotelId, roomId = mappedRoom.RoomId }, 
                mappedRoom);
        }

        [HttpPut]
        [Route("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> UpdateHotelRoom(int hotelId, int roomId, [FromBody] RoomPostPutDto updatedRoom)
        {
            var toUpdate = _mapper.Map<Room>(updatedRoom);
            toUpdate.RoomId = roomId;
            toUpdate.HotelId = hotelId;

            _ctx.Rooms.Update(toUpdate);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> RemoveRoomFromHotel(int hotelId, int roomId)
        {
            var room = await _ctx.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId && r.HotelId == hotelId);

            if (room == null)
            {
                return NotFound("Room not found");
            }

            _ctx.Rooms.Remove(room);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }
    }
}
