using BankingSystem.Data.Access.Context.Interfaces;
using System;
using System.Linq;

namespace BankingSystem.Services.BankManagement
{
    public class BankWorkerService : IBankWorkerService
    {
        private readonly IBankContext _context;

        public BankWorkerService(IBankContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int GetBankIdOfCurrentWorker(Guid guid)
        {
            var bankWorkerBank = _context.BanksOfBankWorker.FirstOrDefault(b => b.WorkerGuid == guid);
            return bankWorkerBank.BankId;
        }
    }
}
