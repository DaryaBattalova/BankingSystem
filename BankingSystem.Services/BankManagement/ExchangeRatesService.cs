using AutoMapper;
using BankingSystem.Data.Access.Context.Interfaces;
using BankingSystem.Models.BankManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Services.BankManagement
{
    public class ExchangeRatesService : IExchangeRatesService
    {
        private readonly IExchangeRatesContext _context;

        public ExchangeRatesService(IExchangeRatesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task SaveExchangeRatesAsync(ExchangeRates exchangeRates)
        {
            _context.ExchangeRates.Add(Mapper.Map<Data.Access.BankManagement.ExchangeRates>(exchangeRates));
            return _context.SaveChangesAsync();
        }

        public Task RemoveExchangeRatesByBankIdAsync(int bankId)
        {
            var rate = _context.ExchangeRates.FirstOrDefault(r => r.BankId == bankId);
            if (rate == null)
            {
                return _context.SaveChangesAsync(); ;
            }

            _context.ExchangeRates.Remove(rate);
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExchangeRates>> GetExchangeRates()
        {
            var exchangeRates = _context.ExchangeRates;
            var result = Mapper.Map<IEnumerable<BankingSystem.Models.BankManagement.ExchangeRates>>(exchangeRates);
            return result.ToArray();
        }

        public double ExchangeCurrency(int bankId, double sumOfMoney, string from, string to)
        {
            var exchangeRates = GetExchangeRatesByBankId(bankId);

            switch (to)
            {
                case "BYN":
                    double sum = ToBYN(exchangeRates, bankId, sumOfMoney, from);
                    Math.Round(sum, 3);
                    return sum;
                case "USD":
                    return Math.Round(ToUSD(exchangeRates, bankId, sumOfMoney, from), 3);
                case "EUR":
                    return Math.Round(ToEUR(exchangeRates, bankId, sumOfMoney, from), 3);
            }

            return sumOfMoney;
        }

        public ExchangeRates GetExchangeRatesByBankId(int bankId)
        {
            var exchangeRates = _context.ExchangeRates.FirstOrDefault(rate => rate.BankId == bankId);
            return Mapper.Map<ExchangeRates>(exchangeRates);
        }

        private double ToBYN(ExchangeRates exchangeRates, int bankId, double sumOfMoney, string from)
        {
            switch (from)
            {
                case "USD":
                    return sumOfMoney * exchangeRates.USDPurchase;
                case "EUR":
                    return sumOfMoney * exchangeRates.EURPurchase;
                default:
                    return sumOfMoney;
            }
        }

        private double ToUSD(ExchangeRates exchangeRates, int bankId, double sumOfMoney, string from)
        {
            switch (from)
            {
                case "BYN":
                    return sumOfMoney / exchangeRates.USDSale;
                case "EUR":
                    double eurToBYN = sumOfMoney * exchangeRates.EURPurchase;
                    return eurToBYN / exchangeRates.USDSale;

                default:
                    return sumOfMoney;
            }
        }

        private double ToEUR(ExchangeRates exchangeRates, int bankId, double sumOfMoney, string from)
        {
            switch (from)
            {
                case "BYN":
                    return sumOfMoney / exchangeRates.EURSale;
                case "USD":
                    {
                        double usdToBYN = sumOfMoney * exchangeRates.USDPurchase;
                        return usdToBYN / exchangeRates.EURSale;
                    }

                default:
                    return sumOfMoney;
            }
        }
    }
}
