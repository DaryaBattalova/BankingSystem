using System.Data.Entity.Migrations;

namespace BankingSystem.Data.Access.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context.ApplicationDbContext>
    {
        public Configuration()
        {
            ContextKey = "ApplicationData";
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(Context.ApplicationDbContext context)
        {
        }
    }
}
