using BankingSystem.Services.BankManagement;
using BankingSystem.Services.Identity;
using BankingSystem.Services.Security;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace BankingSystem.Controllers
{
    [RoutePrefix("api/v1/bankWorker")]
    public class BankWorkerController : ApiController
    {
        private readonly IBankWorkerService _bankWorkerService;
        private readonly IUserContextService _userContextService;
        private readonly ICurrencyExchangeTicketsService _ticketService;
        private readonly ApplicationUserManager _userManager;

        public BankWorkerController(ApplicationUserManager userManager, ICurrencyExchangeTicketsService ticketService, IUserContextService userContextService, IBankWorkerService bankWorkerService)
        {
            _bankWorkerService = bankWorkerService ?? throw new ArgumentNullException(nameof(bankWorkerService));
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpGet]
        [Route("getTicketsForCurrentBankWorker")]
        public async Task<IHttpActionResult> GetTicketsForCurrentBankWorker()
        {
            string date = DateTime.Now.ToShortDateString();
            try
            {
                var user = _userContextService.GetCurrentUser();
                int bankId = _bankWorkerService.GetBankIdOfCurrentWorker(user.Id);
                var tickets = _ticketService.GetTicketsByDateAndBankId(date, bankId);
                List<Models.TicketForWorker> ticketsForWorker = new List<Models.TicketForWorker>();

                foreach (var t in tickets)
                {
                    var userProfile = await _userManager.GetProfileAsync(t.ClientGuid);

                    Models.TicketForWorker ticket = new Models.TicketForWorker
                    {
                        ClientName = userProfile.FirstName,
                        ClientSurname = userProfile.LastName,
                        Time = t.Time,
                        Date = t.Date,
                     };

                    ticketsForWorker.Add(ticket);
                }

                return Ok(ticketsForWorker);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}