using AutoMapper;
using HotelsBooking.Api.Dtos;
using HotelsBooking.Domain.Abstractions.Services;
using HotelsBooking.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelsBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            this._reservationService = reservationService;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> MakeReservation([FromBody] ReservationPutPostDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            var result = await _reservationService.MakeReservationAsync(reservation);

            if(result == null)
            {
                return BadRequest("Cannot create reservation");
            }

            var mapped = _mapper.Map<ReservationGetDto>(result);

            return Ok(mapped);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            var mapped = _mapper.Map<List<ReservationGetDto>>(reservations);
            return Ok(mapped);
        }
    }
}
