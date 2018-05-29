using System;

namespace BankingSystem.Models
{
    public class TicketInfo
    {
        /// <summary>
        /// Gets or sets a bank identifier to which a ticket is related to.
        /// </summary>
        public int BankId { get; set; }

        /// <summary>
        /// Gets or sets a date of a ticket.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets a time of a ticket.
        /// </summary>
        public string Time { get; set; }
    }
}