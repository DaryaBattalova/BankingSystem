using System;

namespace BankingSystem.Data.Access.BankManagement
{
    /// <summary>
    /// Represents a currency exchange ticket.
    /// </summary>
    public class CurrencyExchangeTicket
    {
        /// <summary>
        /// Gets or sets a currency exchange ticket identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a bank identifier to which a ticke is related to.
        /// </summary>
        public int BankId { get; set; }

        /// <summary>
        /// Gets or sets a date to which a ticket is booked.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets a time to which a ticket is booked.
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets an id of a client who booked a ticket.
        /// </summary>
        public Guid ClientGuid { get; set; }

        /// <summary>
        /// Gets or sets a bank to which a ticke is related to.
        /// </summary>
        public virtual Bank Bank { get; set; }
    }
}
