using BankingSystem.Data.Access.BankManagement;
using System.Data.Entity.ModelConfiguration;

namespace BankingSystem.Data.Access.Configurations
{
    /// <summary>
    /// Represents a bank table configuration for Entity Framework.
    /// </summary>
    internal sealed class BankConfiguration : EntityTypeConfiguration<Bank>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankConfiguration"/> class.
        /// </summary>
        public BankConfiguration()
        {
            ToTable("Banks");
            HasKey<int>(i => i.Id);
            Property(i => i.Name).IsRequired();
            Property(i => i.Address).IsRequired();
        }
    }
}
