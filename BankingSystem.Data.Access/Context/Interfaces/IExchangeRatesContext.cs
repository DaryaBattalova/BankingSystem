using BankingSystem.Data.Access.BankManagement;
using System.Threading.Tasks;

namespace BankingSystem.Data.Access.Context.Interfaces
{
    /// <summary>
    /// Represents an exchange rates context.
    /// </summary>
    public interface IExchangeRatesContext
    {
        /// <summary>
        /// Gets a set of exchange rates.
        /// </summary>
        IEntitySet<ExchangeRates> ExchangeRates { get; }

        /// <summary>
        /// Saves all changes made in this context to an underlying storage.
        /// </summary>
        /// <returns>A task result of saving to a data base.</returns>
        Task SaveChangesAsync();
    }
}
