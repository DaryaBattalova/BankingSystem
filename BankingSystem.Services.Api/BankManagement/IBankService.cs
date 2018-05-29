using BankingSystem.Models.BankManagement;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingSystem.Services.BankManagement
{
    public interface IBankService
    {
        Task SaveBankAsync(Bank bank);

        Task<IEnumerable<Bank>> GetBanks();

        Bank GetBankById(int id);
    }
}
