using AutoMapper;
using TicketEase.Service.TicketPurchase.Entities;
using TicketEase.Service.TicketPurchase.Models;

namespace TicketEase.Service.TicketPurchase.Profiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Entities.Ticket, Models.TicketDto>()
                .ForMember(dest => dest.TotalPrice, opts => opts.MapFrom(src => src.Function.Price + src.AdditionalPrice));
        }
    }
}
