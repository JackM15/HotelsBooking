using HotelsBooking.Domain.Models;
using System.Collections.Generic;

namespace HotelsBooking.Api
{
    public class DataSource
    {
        public DataSource()
        {
            Hotels = GetHotels();
        }

        public List<Hotel> Hotels { get; set; }

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
