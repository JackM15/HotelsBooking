using AutoMapper;
using HotelsBooking.Api.Dtos;
using HotelsBooking.Domain.Models;

namespace HotelsBooking.Api.Automapper
{
    public class RoomMappingProfiles : Profile
    {
        public RoomMappingProfiles()
        {
            CreateMap<Room, RoomGetDto>();
            CreateMap<RoomPostPutDto, Room>();
        }
    }
}
