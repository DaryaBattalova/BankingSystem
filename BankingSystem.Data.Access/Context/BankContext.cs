using System;
using System.Threading.Tasks;
using BankingSystem.Data.Access.BankManagement;
using BankingSystem.Data.Access.Context.Interfaces;

namespace BankingSystem.Data.Access.Context
{
    public class BankContext : IBankContext
    {
        private readonly ApplicationDbContext _context;

        public BankContext(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Banks = new EntitySet<Bank>(_context.Banks);
            BanksOfBankWorker = new EntitySet<BankOfBankWorker>(_context.BankOfBankWorker);
        }

        public IEntitySet<Bank> Banks { get; }

        public IEntitySet<BankOfBankWorker> BanksOfBankWorker { get; }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
