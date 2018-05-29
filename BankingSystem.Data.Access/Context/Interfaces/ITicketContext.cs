using BankingSystem.Data.Access.BankManagement;
using System.Threading.Tasks;

namespace BankingSystem.Data.Access.Context.Interfaces
{
    /// <summary>
    /// Represents a ticket context.
    /// </summary>
    public interface ITicketContext
    {
        /// <summary>
        /// Gets a set of tickets.
        /// </summary>
        IEntitySet<CurrencyExchangeTicket> Tickets { get; }

        /// <summary>
        /// Gets a set of possible time to take a ticket.
        /// </summary>
        IEntitySet<TicketTime> TicketTime { get; }

        /// <summary>
        /// Saves all changes made in this context to an underlying storage.
        /// </summary>
        /// <returns>A task result of saving to a data base.</returns>
        Task SaveChangesAsync();
    }
}
