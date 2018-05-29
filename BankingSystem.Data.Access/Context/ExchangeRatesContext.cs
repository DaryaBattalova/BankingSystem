using System;
using System.Threading.Tasks;
using BankingSystem.Data.Access.BankManagement;
using BankingSystem.Data.Access.Context.Interfaces;

namespace BankingSystem.Data.Access.Context
{
    public class ExchangeRatesContext : IExchangeRatesContext
    {
        private readonly ApplicationDbContext _context;

        public ExchangeRatesContext(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            ExchangeRates = new EntitySet<ExchangeRates>(_context.ExchangeRates);
        }

        public IEntitySet<ExchangeRates> ExchangeRates { get; }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
