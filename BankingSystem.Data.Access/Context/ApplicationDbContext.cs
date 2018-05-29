using System.Data.Entity;
using System.Reflection;
using BankingSystem.Data.Access.BankManagement;
using BankingSystem.Data.Access.Migrations;

namespace BankingSystem.Data.Access.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<ExchangeRates> ExchangeRates { get; set; }

        public DbSet<CurrencyExchangeTicket> Tickets { get; set; }

        public DbSet<TicketTime> TicketTime { get; set; }

        public DbSet<BankOfBankWorker> BankOfBankWorker { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
