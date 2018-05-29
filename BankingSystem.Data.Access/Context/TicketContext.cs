using System;
using System.Threading.Tasks;
using BankingSystem.Data.Access.BankManagement;
using BankingSystem.Data.Access.Context.Interfaces;

namespace BankingSystem.Data.Access.Context
{
    public class TicketContext : ITicketContext
    {
        private readonly ApplicationDbContext _context;

        public TicketContext(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Tickets = new EntitySet<CurrencyExchangeTicket>(_context.Tickets);
            TicketTime = new EntitySet<TicketTime>(_context.TicketTime);
        }

        public IEntitySet<CurrencyExchangeTicket> Tickets { get; }

        public IEntitySet<TicketTime> TicketTime { get; }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
