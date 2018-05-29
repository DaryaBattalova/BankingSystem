using BankingSystem.Data.Access.BankManagement;
using System.Data.Entity.ModelConfiguration;

namespace BankingSystem.Data.Access.Configurations
{
    /// <summary>
    /// Represents an invite table configuration for Entity Framework.
    /// </summary>
    public class ExchangeRatesConfiguration : EntityTypeConfiguration<ExchangeRates>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeRatesConfiguration"/> class.
        /// </summary>
        public ExchangeRatesConfiguration()
        {
            ToTable("ExchangeRates");
            HasKey<int>(i => i.Id);
            HasRequired(i => i.Bank);
            Property(i => i.BankId);
            Property(i => i.USDPurchase).IsRequired();
            Property(i => i.USDSale).IsRequired();
            Property(i => i.EURPurchase).IsRequired();
            Property(i => i.EURSale).IsRequired();
        }
    }
}
