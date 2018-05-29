using BankingSystem.Services.BankManagement;
using BankingSystem.Services.UserManagement;
using Ninject.Modules;

namespace BankingSystem.Services
{
    /// <summary>
    /// Represents a registration module for NInject.
    /// </summary>
    public class RegistrationModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IEmailSendingService>().To<EmailSendingService>();
            Bind<IBankService>().To<BankService>();
            Bind<IExchangeRatesService>().To<ExchangeRatesService>();
            Bind<ICurrencyExchangeTicketsService>().To<CurrencyExchangeTicketsService>();
            Bind<IBankWorkerService>().To<BankWorkerService>();
        }
    }
}
