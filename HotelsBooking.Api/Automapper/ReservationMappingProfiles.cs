using AutoMapper;
using HotelsBooking.Api.Dtos;
using HotelsBooking.Domain.Models;

namespace HotelsBooking.Api.Automapper
{
    public class ReservationMappingProfiles : Profile
    {
        public ReservationMappingProfiles()
        {
            CreateMap<ReservationPutPostDto, Reservation>();
            CreateMap<Reservation, ReservationGetDto>();
        }
    }
}
