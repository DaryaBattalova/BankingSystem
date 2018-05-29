using AutoMapper;
using EntityTicketTime = BankingSystem.Data.Access.BankManagement.TicketTime;
using ModelTicketTime = BankingSystem.Models.BankManagement.TicketTime;

namespace BankingSystem.Services.Mapping
{
    internal class TicketTimeMappingProfile : Profile
    {
        public TicketTimeMappingProfile()
        {
            CreateMap<EntityTicketTime, ModelTicketTime>();
            CreateMap<ModelTicketTime, EntityTicketTime>();
        }
    }
}
