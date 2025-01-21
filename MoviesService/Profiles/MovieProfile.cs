using AutoMapper;

namespace TicketEase.Service.Movies.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Entities.Movie, Models.MovieDto>().ReverseMap();
        }
    }
}
