using HotelsBooking.Dal;
using HotelsBooking.Domain.Abstractions.Repositories;
using HotelsBooking.Domain.Abstractions.Services;
using HotelsBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelsBooking.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IHotelsRepository _hotelsRepository;
        private readonly DataContext _ctx;

        public ReservationService(IHotelsRepository hotelsRepository, DataContext ctx)
        {
            this._hotelsRepository = hotelsRepository;
            this._ctx = ctx;
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await _ctx.Reservations.Include(r => r.Hotel).Include(r => r.Room).ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int reservationId)
        {
            return await _ctx.Reservations
                .Include(r => r.Hotel)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(r => r.ReservationId == reservationId);
        }

        public async Task<Reservation> MakeReservationAsync(Reservation reservation)
        {

            // Get the hotel, including all rooms
            var hotel = await _hotelsRepository.GetHotelByIdAsync(reservation.HotelId);

            // Find the specified room
            var room = hotel.Rooms.Where(r => r.RoomId == reservation.RoomId).FirstOrDefault();

            if (hotel == null || room == null) return null;

            // Check room availability
            bool isBusy = await _ctx.Reservations.AnyAsync(r => 
                (reservation.CheckInDate >= r.CheckInDate && reservation.CheckInDate <= r.CheckOutDate)
                && (reservation.CheckOutDate >= r.CheckInDate && reservation.CheckOutDate <= r.CheckOutDate)
            );


            if (isBusy)
            {
                return null;
            }

            if (room.NeedsRepair)
            {
                return null;
            }


            // Persist all changes to the DB
            _ctx.Rooms.Update(room);
            _ctx.Reservations.Add(reservation);
            await _ctx.SaveChangesAsync();

            // Return the reservation
            return reservation;
        }
    }
}
