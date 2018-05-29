using BankingSystem.Data.Access.BankManagement;
using System.Data.Entity.ModelConfiguration;

namespace BankingSystem.Data.Access.Configurations
{
    /// <summary>
    /// Represents an TicketTime table configuration for Entity Framework.
    /// </summary>
    public class TicketTimeConfiguration : EntityTypeConfiguration<TicketTime>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TicketTimeConfiguration"/> class.
        /// </summary>
        public TicketTimeConfiguration()
        {
            ToTable("TicketTime");
            HasKey<int>(i => i.Id);
            Property(i => i.Time).IsRequired();
        }
    }
}
