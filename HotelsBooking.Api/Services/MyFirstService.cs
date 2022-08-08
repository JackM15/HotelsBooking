using HotelsBooking.Domain.Models;
using System.Collections.Generic;

namespace HotelsBooking.Api.Services
{
    public class MyFirstService
    {
        private readonly DataSource _dataSource;

        public MyFirstService(DataSource dataSource)
        {
            this._dataSource = dataSource;
        }

        public List<Hotel> GetHotels()
        {
            return _dataSource.Hotels;
        }
    }
}
