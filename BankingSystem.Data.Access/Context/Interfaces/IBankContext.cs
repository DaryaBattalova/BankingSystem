using BankingSystem.Data.Access.BankManagement;
using System.Threading.Tasks;

namespace BankingSystem.Data.Access.Context.Interfaces
{
    /// <summary>
    /// Represents a bank context.
    /// </summary>
    public interface IBankContext
    {
        /// <summary>
        /// Gets a set of banks.
        /// </summary>
        IEntitySet<Bank> Banks { get; }

        /// <summary>
        /// Gets a set of BanksOfBankWorker.
        /// </summary>
        IEntitySet<BankOfBankWorker> BanksOfBankWorker { get; }

        /// <summary>
        /// Saves all changes made in this context to an underlying storage.
        /// </summary>
        /// <returns>A task result of saving to a data base.</returns>
        Task SaveChangesAsync();
    }
}
