using AutoMapper;
using EntityBank = BankingSystem.Data.Access.BankManagement.Bank;
using ModelBank = BankingSystem.Models.BankManagement.Bank;

namespace BankingSystem.Services.Mapping
{
    internal class BankMappingProfile : Profile
    {
        public BankMappingProfile()
        {
            CreateMap<EntityBank, ModelBank>();
            CreateMap<ModelBank, EntityBank>();
        }
    }
}
