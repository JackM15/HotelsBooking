using AutoMapper;
using HotelsBooking.Api.Dtos;
using HotelsBooking.Domain.Models;

namespace HotelsBooking.Api.Automapper
{
    public class HotelMappingProfiles : Profile
    {
        public HotelMappingProfiles()
        {
            //map a hotel create dto to a hotel
            CreateMap<HotelCreateDto, Hotel>();
            //map a hotel to a hotel get dto
            CreateMap<Hotel, HotelGetDto>();
        }
    }
}
