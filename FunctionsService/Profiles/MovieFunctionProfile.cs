using AutoMapper;
using TicketEase.Service.Functions.Entities;
using TicketEase.Service.Functions.Models;

namespace TicketEase.Service.Functions.Profiles
{
    public class MovieFunctionProfile : Profile
    {
        public MovieFunctionProfile()
        {
            CreateMap<MovieFunction, MovieFunctionDto>().ReverseMap();
            CreateMap<MovieFunction, MovieFunctionDto>().ReverseMap();
        }
    }
}
