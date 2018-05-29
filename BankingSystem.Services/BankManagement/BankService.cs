using AutoMapper;
using BankingSystem.Data.Access.Context.Interfaces;
using BankingSystem.Models.BankManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Services.BankManagement
{
    public class BankService : IBankService
    {
        private readonly IBankContext _context;

        public BankService(IBankContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task SaveBankAsync(BankingSystem.Models.BankManagement.Bank bank)
        {
            _context.Banks.Add(Mapper.Map<Data.Access.BankManagement.Bank>(bank));
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Bank>> GetBanks()
        {
            var banks = _context.Banks;
            var result = Mapper.Map<IEnumerable<BankingSystem.Models.BankManagement.Bank>>(banks);
            return result.ToArray();
        }

        public Bank GetBankById(int id)
        {
            var bank = _context.Banks.FirstOrDefault(b => b.Id == id);
            return Mapper.Map<Bank>(bank);
        }
    }
}
