using System.Collections.Generic;
using System.Threading.Tasks;
using HotelApp.Api.Models;
using Microsoft.AspNetCore.Http;

namespace HotelApp.Api.Repositorys
{
    public interface IHotelRepository
    {
        // --------- Hotels ---------
         List<Hotel> GetHotels();
         Hotel GetHotel(int id);
         void AddHotel(Hotel hotel);
         void UpdateHotel(Hotel hotel);
         void DeleteHotel(Hotel hotel);
         // ---------- Rooms --------
         List<Room> GetRooms();
         List<Room> GetRooms(int hotelId);
         Room GetRoom(int id);
         void AddRoom(Room room);
         void UpdateRoom(Room room);
         void DeleteRoom(Room room);
         //-------- Reservition -------------
         List<Reservation> GetReservations();
         Reservation GetReservation(int id);
         void AddReservation(Reservation reservation);
         void UpdateReservation(Reservation reservation);
         void DeleteReservation(Reservation reservation);
         
        // ------- PointOfInterests -----------

        List<PointOfInterest> GetPointOfInterests();
        PointOfInterest GetPointOfInterest(int id);
        void AddPointOfInterest(PointOfInterest pointOfInterest);
        void UpdatePointOfInterest(PointOfInterest pointOfInterest);
    }

}