using HotelsBooking.Domain.Models;

namespace HotelsBooking.Api.Dtos
{
    public class RoomPostPutDto
    {
        public int RoomNumber { get; set; }
        public double Surface { get; set; }
        public bool NeedsRepair { get; set; }
    }
}
