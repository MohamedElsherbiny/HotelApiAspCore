using System.Collections.Generic;
using HotelApp.Api.Dtos;
using AutoMapper;
using HotelApp.Api.Models;

namespace HotelApp.Extentions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<List<User>,UserForListDto>();
            CreateMap<User,UserForListDto>();
        }
    }
}