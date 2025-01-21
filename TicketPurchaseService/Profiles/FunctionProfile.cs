using AutoMapper;

namespace TicketEase.Service.TicketPurchase.Profiles
{
    public class FunctionProfile : Profile
    {
        public FunctionProfile()
        {
            CreateMap<Entities.Function, Models.FunctionDto>().ReverseMap();
        }
    }
}
