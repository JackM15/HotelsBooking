using HotelsBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelsBooking.Dal
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }    
    }
}
