using System.Collections.Generic;
using HotelApp.Api.Dtos;
using AutoMapper;
using HotelApp.Api.Models;

namespace HotelApp.Api.Extentions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<List<User>,UserForListDto>();
            CreateMap<User,UserForListDto>();

            CreateMap<HotelDto,Hotel>().ReverseMap();
            CreateMap<Hotel,HotelListDto>().ReverseMap();

            CreateMap<RoomDto,Room>().ReverseMap();
            CreateMap<Room,RoomListDto>().ReverseMap();

            CreateMap<ReservationDto,Reservation>().ReverseMap();
            CreateMap<Reservation,ReservationListDto>().ReverseMap();

            CreateMap<PointOfInterest,PointOfInterestDto>().ReverseMap();
            CreateMap<PointOfInterestDto,PointOfInterest>().ReverseMap();
        }
    }
}