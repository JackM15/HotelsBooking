using HotelsBooking.Domain.Abstractions.Repositories;
using HotelsBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HotelsBooking.Dal.Repositories
{
    public class HotelsRepository : IHotelsRepository
    {
        private readonly DataContext _ctx;

        public HotelsRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Hotel> CreateHotelAsync(Hotel hotel)
        {
            //Add in memory (no db call)
            _ctx.Hotels.Add(hotel);
            //Save all context changes to DB
            await _ctx.SaveChangesAsync();
            //Return the hotel
            return hotel;
        }

        public async Task<Room> CreateHotelRoomAsync(int hotelId, Room room)
        {
            //Get the hotel using the id and include the rooms.
            var hotel = await _ctx.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.HotelId == hotelId);
            //Add the new room to the rooms
            hotel.Rooms.Add(room);
            //Save to db
            await _ctx.SaveChangesAsync();
            //Return the room
            return room;
        }

        public async Task<Hotel> DeleteHotelAsync(int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);

            if (hotel == null)
            {
                return null;
            }

            //Remove hotel in memory/context
            _ctx.Hotels.Remove(hotel);
            //Save changes to DB
            await _ctx.SaveChangesAsync();     
            //Return deleted hotel
            return hotel;
        }

        public async Task<Room> DeleteHotelRoomAsync(int hotelId, int roomId)
        {
            var room = await _ctx.Rooms.FirstOrDefaultAsync(r => r.RoomId == roomId && r.HotelId == hotelId);

            if (room == null)
            {
                return null;
            }

            _ctx.Rooms.Remove(room);
            await _ctx.SaveChangesAsync();
            return room;
        }

        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            var hotels = await _ctx.Hotels.ToListAsync();
            return hotels;
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);

            if (hotel == null)
            {
                return null;
            }

            return hotel;
        }

        public async Task<Room> GetHotelRoomByIdAsync(int hotelId, int roomId)
        {
            var room = await _ctx.Rooms.FirstOrDefaultAsync(r => r.HotelId == hotelId && r.RoomId == roomId);
            if (room == null)
            {
                return null;
            }
            return room;
        }

        public async Task<List<Room>> ListHotelRoomsAsync(int hotelId)
        {
            var rooms = await _ctx.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();
            return rooms;
        }

        public async Task<Hotel> UpdateHotelAsync(Hotel updatedHotel)
        {
            //Tell db context to update the data
            _ctx.Hotels.Update(updatedHotel);

            //Call DB to update changes
            await _ctx.SaveChangesAsync();
            return updatedHotel;
        }

        public async Task<Room> UpdateHotelRoomAsync(int hotelId, Room updatedRoom)
        {
            _ctx.Rooms.Update(updatedRoom);
            await _ctx.SaveChangesAsync();
            return updatedRoom;
        }
    }
}
