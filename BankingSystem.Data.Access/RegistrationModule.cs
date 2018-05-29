using BankingSystem.Data.Access.Context;
using BankingSystem.Data.Access.Context.Interfaces;
using Ninject.Modules;

namespace BankingSystem.Data.Access
{
    /// <summary>
    /// Represents a registration module for Ninject.
    /// </summary>
    public class RegistrationModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<ApplicationDbContext>().To<ApplicationDbContext>();
            Bind<IBankContext>().To<BankContext>();
            Bind<IExchangeRatesContext>().To<ExchangeRatesContext>();
            Bind<ITicketContext>().To<TicketContext>();
        }
    }
}
