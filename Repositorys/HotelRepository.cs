using System.Collections.Generic;
using System.Linq;
using HotelApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.Api.Repositorys
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelDbContext _context;
        private readonly AppDbContext _userContext;

        public HotelRepository(HotelDbContext context,AppDbContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }
        public void AddHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
        }

        public void AddPointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointOfInterests.Add(pointOfInterest);
            _context.SaveChanges();
        }

        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public void AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void DeleteHotel(Hotel hotel)
        {
            _context.Hotels.Remove(hotel);
            _context.SaveChanges();
        }

        public void DeleteReservation(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }

        public void DeleteRoom(Room room)
        {
            _context.Rooms.Remove(room);
            _context.SaveChanges();
        }

        public Hotel GetHotel(int id)
        {
            var hotel = _context.Hotels
                        .Include(x => x.PointOfInterests)
                        .Include(x => x.Reservations)
                        .Include(x => x.Rooms)
                        .FirstOrDefault(x => x.Id == id);
            return hotel;
        }

        public List<Hotel> GetHotels()
        {
            return _context.Hotels
                    .Include(x => x.PointOfInterests)
                    .Include(x => x.Reservations)
                    .Include(x => x.Rooms)
                    .ToList();
        }

        public PointOfInterest GetPointOfInterest(int id)
        {
            var pointOfInterest = _context.PointOfInterests.FirstOrDefault(x => x.Id == id);
            return pointOfInterest;
        }

        public List<PointOfInterest> GetPointOfInterests()
        {
            return _context.PointOfInterests.ToList();
        }

        public Reservation GetReservation(int id)
        {
            var reservation = _context.Reservations.FirstOrDefault(x => x.Id == id);
            return reservation;
        }

        public List<Reservation> GetReservations()
        {
            return _context.Reservations
                    .Include(x => x.Room).ToList();
        }

        public Room GetRoom(int id)
        {
            var room = _context.Rooms.FirstOrDefault(x => x.RoomId == id);
            return room;
        }

        public List<Room> GetRooms()
        {
            return _context.Rooms.Include(x => x.Hotel).ToList();
        }
        public List<Room> GetRooms(int hotelId)
        {
            return _context.Rooms.Where(z => z.HotelId == hotelId).Include(x => x.Hotel).ToList();
        }


        public void UpdateHotel(Hotel hotel)
        {
            var hotelToUpdate = _context.Hotels.FirstOrDefault(x => x.Id == hotel.Id);
            hotelToUpdate.Name = hotel.Name;
            hotelToUpdate.Phone = hotel.Phone;
            hotelToUpdate.Street = hotel.Street;
            hotelToUpdate.City = hotel.City;
            hotelToUpdate.Country = hotel.Country;
            hotelToUpdate.State = hotel.State;
            hotelToUpdate.Zip = hotel.Zip;
            hotelToUpdate.Coordinates = hotel.Coordinates;
            hotelToUpdate.Stars = hotel.Stars;
            _context.Hotels.Update(hotelToUpdate);
            _context.SaveChanges();
            
            
            
            
            
            
            
        }

        public void UpdatePointOfInterest(PointOfInterest pointOfInterest)
        {
            var pointOfInterestToUpdate = _context.PointOfInterests.FirstOrDefault(x => x.Id == pointOfInterest.Id);
            pointOfInterestToUpdate.Name = pointOfInterest.Name;
            pointOfInterestToUpdate.Description = pointOfInterest.Description;
            _context.PointOfInterests.Update(pointOfInterestToUpdate);
            _context.SaveChanges();
        }

        public void UpdateReservation(Reservation reservation)
        {
            var reservationToUpdate = _context.Reservations.FirstOrDefault(x => x.Id == reservation.Id);
            reservationToUpdate.RoomId = reservation.RoomId;
            reservationToUpdate.GustName = reservation.GustName;
            reservationToUpdate.CheckInDate = reservation.CheckInDate;
            reservationToUpdate.CheckOutDate = reservation.CheckOutDate;
            reservationToUpdate.ConfirmKey = reservation.ConfirmKey;
            _context.Reservations.Update(reservationToUpdate);
            _context.SaveChanges();
        }

        public void UpdateRoom(Room room)
        {
            var roomToUpdate = _context.Rooms.FirstOrDefault(x => x.RoomId == room.RoomId);
            roomToUpdate.Rate = room.Rate;
            roomToUpdate.IsAvailable = room.IsAvailable;
            roomToUpdate.HotelId = room.HotelId;
            _context.Rooms.Update(roomToUpdate);
            _context.SaveChanges();
        }
    }
}