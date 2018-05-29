using BankingSystem.Models.BankManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingSystem.Services.BankManagement
{
    public interface IExchangeRatesService
    {
        Task SaveExchangeRatesAsync(ExchangeRates exchangeRates);

        Task<IEnumerable<ExchangeRates>> GetExchangeRates();

        double ExchangeCurrency(int bankId, double sumOfMoney, string from, string to);

        ExchangeRates GetExchangeRatesByBankId(int bankId);

        Task RemoveExchangeRatesByBankIdAsync(int bankId);
    }
}
