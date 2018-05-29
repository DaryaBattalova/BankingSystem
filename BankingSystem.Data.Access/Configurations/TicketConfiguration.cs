using BankingSystem.Data.Access.BankManagement;
using System.Data.Entity.ModelConfiguration;

namespace BankingSystem.Data.Access.Configurations
{
    public class TicketConfiguration : EntityTypeConfiguration<CurrencyExchangeTicket>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TicketConfiguration"/> class.
        /// </summary>
        public TicketConfiguration()
        {
            ToTable("CurrencyExchangeTickets");
            HasKey<int>(i => i.Id);
            HasRequired(i => i.Bank);
            Property(i => i.BankId);
            Property(i => i.Date).IsRequired();
            Property(i => i.Time).IsRequired();
            Property(i => i.ClientGuid).IsRequired();
        }
    }
}
