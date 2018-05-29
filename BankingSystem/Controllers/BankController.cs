using BankingSystem.Models.BankManagement;
using BankingSystem.Services.BankManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace BankingSystem.Controllers
{
    [RoutePrefix("api/v1/banks")]
   // [Authorize(Roles = GlobalInfo.Client)]
    public class BankController : ApiController
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService)
        {
            _bankService = bankService ?? throw new ArgumentNullException(nameof(bankService));
        }

        /// <summary>
        /// Gets an IEnumerable of banks.
        /// </summary>
        /// <returns>An IEnumerable of banks.</returns>
        [HttpGet]
        [Route("bankList")]
        [ResponseType(typeof(IEnumerable<Bank>))]
        public IHttpActionResult GetBanksInfo()
        {
            try
            {
                return Ok(_bankService.GetBanks().Result);
            }
            catch (Exception)
            {
               return InternalServerError();
            }
        }

        [HttpPost]
        [Route("createBank")]
        public async Task<IHttpActionResult> CreateBank(Models.BankInfo bank)
        {
            Bank b = new Bank
            {
                Name = bank.Name,
                Address = bank.Address,
            };

            try
            {
               await _bankService.SaveBankAsync(b);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok();
        }

        [HttpPost]
        [Route("seedBanks")]
        public async Task<IHttpActionResult> SeedBanks()
        {
            Bank BelarusBank = new Bank
            {
                Name = "BelarusBank",
                Address = "Surganova, 66",
            };

            Bank BTABank = new Bank
            {
                Name = "BTA Bank",
                Address = "Horuzej, 20-2",
            };

            Bank BelGazPromBank = new Bank
            {
                Name = "BelGazPromBank",
                Address = "Bogdanovicha, 116",
            };

            try
            {
                await _bankService.SaveBankAsync(BelarusBank);
                await _bankService.SaveBankAsync(BTABank);
                await _bankService.SaveBankAsync(BelGazPromBank);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok();
        }

        /// <summary>
        /// Gets a bank by id.
        /// </summary>
        /// <param name="id">An id of a bank.</param>
        /// <returns>A bank found by id.</returns>
        [HttpGet]
        [Route("bank")]
        [ResponseType(typeof(Bank))]
        public IHttpActionResult GetBank(int id)
        {
            try
            {
                return Ok(_bankService.GetBankById(id));
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}