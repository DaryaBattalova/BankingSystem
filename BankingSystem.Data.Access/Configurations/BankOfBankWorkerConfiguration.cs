using BankingSystem.Data.Access.BankManagement;
using System.Data.Entity.ModelConfiguration;

namespace BankingSystem.Data.Access.Configurations
{
    internal sealed class BankOfBankWorkerConfiguration : EntityTypeConfiguration<BankOfBankWorker>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankOfBankWorkerConfiguration"/> class.
        /// </summary>
        public BankOfBankWorkerConfiguration()
        {
            ToTable("BankOfBankWorker");
            HasKey<int>(i => i.Id);
            Property(i => i.BankId).IsRequired();
            Property(i => i.WorkerGuid).IsRequired();
        }
    }
}
