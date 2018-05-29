using AutoMapper;
using EntityTicket = BankingSystem.Data.Access.BankManagement.CurrencyExchangeTicket;
using ModelTicket = BankingSystem.Models.BankManagement.CurrencyExchangeTicket;

namespace BankingSystem.Services.Mapping
{
    internal class TicketMappingProfile : Profile
    {
        public TicketMappingProfile()
        {
            CreateMap<EntityTicket, ModelTicket>();
            CreateMap<ModelTicket, EntityTicket>();
        }
    }
}
