using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using HotelApp.Api.Dtos;
using HotelApp.Api.Models;
using HotelApp.Api.Repositorys;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Api.Controllers
{
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        private readonly IHotelRepository _repo;
        private readonly IMapper _mapper;

        public HotelsController(IHotelRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // ------- Hotels -------

        public IActionResult GetHotels()
        {
            var hotels = _repo.GetHotels();
            var hotelsToReturn = _mapper.Map<List<HotelListDto>>(hotels);
            return Json(hotelsToReturn);
        }
        [HttpGet("{hotelName}")]
        public IActionResult SearchHotels([FromRoute]string hotelName)
        {
            hotelName = hotelName.ToLower();
            var hotels = _repo.GetHotels();
            var filtered = hotels.Where(x => 
                            x.Country.ToLower().Contains(hotelName) ||
                            x.City.ToLower().Contains(hotelName) ||
                            x.Name.ToLower().Contains(hotelName) ||
                            x.Street.ToLower().Contains(hotelName)).ToList();
            var hotelsToReturn = _mapper.Map<List<HotelListDto>>(filtered);
            return Json(hotelsToReturn);
        }
        [HttpGet("get/{id}")]
        public IActionResult GetHotel([FromRoute]int id)
        {
            var hotel = _repo.GetHotel(id);
            if(hotel == null)
                return NotFound();
            var hotelToReturn = _mapper.Map<HotelDto>(hotel);
            return Json(hotelToReturn);
        }
        [HttpPost("Add")]
        public IActionResult AddHotel([FromBody]HotelDto hotelDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var hotel = _mapper.Map<Hotel>(hotelDto);
            _repo.AddHotel(hotel);
            return Ok();
        }
        [HttpPost("Update")]
        public IActionResult UpdateHotel([FromBody]HotelDto hotelDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var hotel = _mapper.Map<Hotel>(hotelDto);
            _repo.UpdateHotel(hotel);
            return Ok();
        }
        [HttpPost("Delete/{id}")]
        public IActionResult DeleteHotel([FromRoute]int id)
        {
            var hotel = _repo.GetHotel(id);
            if(hotel == null)
                return NotFound();
            _repo.DeleteHotel(hotel);
            return Ok();
        }

        // ---------- Rooms ------------
        [HttpGet("Rooms")]
        public IActionResult GetRooms()
        {
            var rooms = _repo.GetRooms();
            var roomsToReturn = _mapper.Map<List<RoomListDto>>(rooms);
            return Json(roomsToReturn);
        }
        [HttpGet("{hotelId}/Rooms")]
        public IActionResult GetRooms([FromRoute]int hotelId)
        {
            var rooms = _repo.GetRooms(hotelId);
            var roomsToReturn = _mapper.Map<List<RoomListDto>>(rooms);
            return Json(roomsToReturn);
        }
        [HttpGet("Rooms/{id}")]
        public IActionResult GetRoom([FromRoute]int id)
        {
            var room = _repo.GetRoom(id);
            if(room == null)
                return NotFound();
            var roomToReturn = _mapper.Map<RoomDto>(room);
            return Json(roomToReturn);
        }
        [HttpPost("Rooms/Add")]
        public IActionResult AddRoom([FromBody]RoomDto roomDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var room  = _mapper.Map<Room>(roomDto);
            _repo.AddRoom(room);
            return Ok();
        }
        [HttpPost("Rooms/Update")]
        public IActionResult UpdateRoom([FromBody]RoomDto roomDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var room = _mapper.Map<Room>(roomDto);
            _repo.UpdateRoom(room);
            return Ok();
        }
        [HttpPost("Rooms/Delete/{id}")]
        public IActionResult DeleteRoom([FromRoute]int id)
        {
            var room = _repo.GetRoom(id);
            if(room == null)
                return NotFound();
            _repo.DeleteRoom(room);
            return Ok();
        }
        // --------- Reservations ------------
        [HttpGet("reservations")]
        public IActionResult GetReservations()
        {
            var reservations = _repo.GetReservations();
            var reservationsToReturn = _mapper.Map<List<ReservationListDto>>(reservations);
            return Json(reservationsToReturn);
        }
        [HttpGet("reservations/{id}")]
        public IActionResult GetReservation([FromRoute]int id)
        {
            var reservation = _repo.GetReservation(id);
            if(reservation == null)
                return NotFound();
            var reservationToReturn = _mapper.Map<ReservationDto>(reservation);
            return Json(reservationToReturn);
        }
        [HttpPost("Reservations/Add")]
        public IActionResult AddReservation([FromBody]ReservationDto reservationDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var reservation = _mapper.Map<Reservation>(reservationDto);
            _repo.AddReservation(reservation);
            return Ok();
        }
        [HttpPost("Reservations/Update")]
        public IActionResult UpdateReservation([FromBody]ReservationDto reservationDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var reservation = _mapper.Map<Reservation>(reservationDto);
            _repo.UpdateReservation(reservation);
            return Ok();
        }
        [HttpPost("Reservations/Delete/{id}")]
        public IActionResult DeleteReservation([FromRoute]int id)
        {
            var reservation = _repo.GetReservation(id);
            if(reservation == null)
                return NotFound();
            _repo.DeleteReservation(reservation);
            return Ok();
        }


        // --------- PointOfInterests -----
        
         [HttpGet("PointOfInterests")]
        public IActionResult GetPointOfInterests()
        {
            var pointOfInterests = _repo.GetPointOfInterests();
            var PointOfInterestsToReturn = _mapper.Map<List<PointOfInterestDto>>(pointOfInterests);
            return Json(PointOfInterestsToReturn);
        }
        [HttpGet("PointOfInterests/{id}")]
        public IActionResult GetPointOfInterest([FromRoute]int id)
        {
            var pointOfInterest = _repo.GetPointOfInterest(id);
            if(pointOfInterest == null)
                return NotFound();
            var PointOfInterestToReturn = _mapper.Map<PointOfInterestDto>(pointOfInterest);
            return Json(PointOfInterestToReturn);
        }

        [HttpPost("PointOfInterests/Add")]
        public IActionResult AddPointOfInterest([FromBody]PointOfInterestDto pointOfInterestDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var PointOfInterest = _mapper.Map<PointOfInterest>(pointOfInterestDto);
            _repo.AddPointOfInterest(PointOfInterest);
            return Ok();
        }
        [HttpPost("PointOfInterests/Update")]
        public IActionResult UpdatePointOfInterest([FromBody]PointOfInterestDto pointOfInterestDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var PointOfInterest = _mapper.Map<PointOfInterest>(pointOfInterestDto);
            _repo.UpdatePointOfInterest(PointOfInterest);
            return Ok();
        }
    }
}