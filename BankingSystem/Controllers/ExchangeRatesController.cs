using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BankingSystem.Models.BankManagement;
using BankingSystem.Services.BankManagement;
using BankingSystem.Services.Security;

namespace BankingSystem.Controllers
{
    [RoutePrefix("api/v1/exchangeRates")]
    // [Authorize(Roles = GlobalInfo.Client)]
    public class ExchangeRatesController : ApiController
    {
        private readonly IExchangeRatesService _exchangeRatesService;
        private readonly IBankService _bankService;
        private readonly IUserContextService _userContextService;
        private readonly IBankWorkerService _bankWorkerService;

        public ExchangeRatesController(IBankWorkerService bankWorkerService, IUserContextService userContextService, IExchangeRatesService exchangeRatesService, IBankService bankService)
        {
            _exchangeRatesService = exchangeRatesService ?? throw new ArgumentNullException(nameof(exchangeRatesService));
            _bankService = bankService ?? throw new ArgumentNullException(nameof(bankService));
            _bankWorkerService = bankWorkerService ?? throw new ArgumentNullException(nameof(bankWorkerService));
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
        }

        [HttpPost]
        [Route("createExchangeRates")]
        public async Task<IHttpActionResult> CreateExchangeRates(Models.ExchangeRatesInfo info)
        {
            var user = _userContextService.GetCurrentUser();
            int bankId = _bankWorkerService.GetBankIdOfCurrentWorker(user.Id);

            string usdPur = info.USDPurchase.Replace('.', ',');
            string eurPur = info.EURPurchase.Replace('.', ',');
            string usdSale = info.USDSale.Replace('.', ',');
            string eurSale = info.EURSale.Replace('.', ',');

            double usdPurchase = Convert.ToDouble(usdPur);
            double eurPurchase = Convert.ToDouble(eurPur);
            double usdSalee = Convert.ToDouble(usdSale);
            double eurSalee = Convert.ToDouble(eurSale);

            ExchangeRates rates = new ExchangeRates
            {
                BankId = bankId,
                USDPurchase = usdPurchase,
                USDSale = usdSalee,
                EURPurchase = eurPurchase,
                EURSale = eurSalee
            };

            try
            {
                await _exchangeRatesService.RemoveExchangeRatesByBankIdAsync(bankId);
                await _exchangeRatesService.SaveExchangeRatesAsync(rates);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpPost]
        [Route("seedExchangeRates")]
        public async Task<IHttpActionResult> SeedExchangeRates()
        {
            ExchangeRates BelarusBankRates = new ExchangeRates
            {
                BankId = 1,
                USDPurchase = 2.00,
                USDSale = 2.013,
                EURPurchase = 2.338,
                EURSale = 2.358
            };

            ExchangeRates BTABankRates = new ExchangeRates
            {
                BankId = 2,
                USDPurchase = 2.002,
                USDSale = 2.012,
                EURPurchase = 2.330,
                EURSale = 2.348
            };

            ExchangeRates BelGazPromBankRates = new ExchangeRates
            {
                BankId = 3,
                USDPurchase = 1.998,
                USDSale = 2.009,
                EURPurchase = 2.335,
                EURSale = 2.354
            };

            try
            {
                await _exchangeRatesService.SaveExchangeRatesAsync(BelarusBankRates);
                await _exchangeRatesService.SaveExchangeRatesAsync(BTABankRates);
                await _exchangeRatesService.SaveExchangeRatesAsync(BelGazPromBankRates);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpPost]
        [Route("exchangeCurrency")]
        public IHttpActionResult ExchangeCurrency([FromBody]Models.CurrencyExchange currencyExchange)
        {
            try
            {
                string s = currencyExchange.SumOfMoney.Replace('.', ',');
                double sum = Convert.ToDouble(s);
                return Ok(_exchangeRatesService.ExchangeCurrency(currencyExchange.BankId,
                    sum, currencyExchange.CurrencyFrom, currencyExchange.CurrencyTo));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Gets an IEnumerable of exchange rates.
        /// </summary>
        /// <returns>An IEnumerable of exchange rates.</returns>
        [HttpGet]
        [Route("exchangeRates")]
        [ResponseType(typeof(IEnumerable<ExchangeRates>))]
        public IHttpActionResult GetExchangeRates()
        {
            try
            {
                return Ok(_exchangeRatesService.GetExchangeRates().Result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("getExchangeRatesByBankId")]
        public IHttpActionResult GetExchangeRatesByBankId()
        {

            try
            {
                var user = _userContextService.GetCurrentUser();
                int bankId = _bankWorkerService.GetBankIdOfCurrentWorker(user.Id);
                return Ok(_exchangeRatesService.GetExchangeRatesByBankId(bankId));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}