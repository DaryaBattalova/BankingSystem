using AutoMapper;
using EntityExchangeRates = BankingSystem.Data.Access.BankManagement.ExchangeRates;
using ModelExchangeRates = BankingSystem.Models.BankManagement.ExchangeRates;

namespace BankingSystem.Services.Mapping
{
    internal class ExchangeRatesMappingProfile : Profile
    {
        public ExchangeRatesMappingProfile()
        {
            CreateMap<EntityExchangeRates, ModelExchangeRates>();
            CreateMap<ModelExchangeRates, EntityExchangeRates>();
        }
    }
}
