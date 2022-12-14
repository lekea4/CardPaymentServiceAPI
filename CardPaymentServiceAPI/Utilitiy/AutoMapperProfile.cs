using AutoMapper;
using CardPaymentServiceAPI.Models;
using CardPaymentServiceAPI.Models.DTOs;

namespace CardPaymentServiceAPI.Utilitiy
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Fintechs, FintechsDto>().ReverseMap();
            CreateMap<CardsDetails,CardsDto>().ReverseMap();
            CreateMap<Payment,PaymentDto>().ReverseMap();
            CreateMap<Payment, PaymentResponseDto>().ReverseMap();

            CreateMap<Payment, CardsDetails>().ReverseMap();
            CreateMap<Payment, CardsDto>().ReverseMap();

        }
        
    }
}
