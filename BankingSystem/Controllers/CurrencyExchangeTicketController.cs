using BankingSystem.Models.BankManagement;
using BankingSystem.Services.BankManagement;
using BankingSystem.Services.Identity;
using BankingSystem.Services.Security;
using BankingSystem.Services.UserManagement;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Http;

namespace BankingSystem.Controllers
{
    [RoutePrefix("api/v1/tickets")]
    public class CurrencyExchangeTicketController : ApiController
    {
        private readonly ICurrencyExchangeTicketsService _ticketService;
        private readonly IUserContextService _userContextService;
        private readonly ApplicationUserManager _userManager;
        private readonly IEmailSendingService _emailSendingService;

        public CurrencyExchangeTicketController(IEmailSendingService emailSendingService, ICurrencyExchangeTicketsService ticketService, IUserContextService userContextService, ApplicationUserManager userManager)
        {
            _ticketService = ticketService ?? throw new ArgumentNullException(nameof(ticketService));
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _emailSendingService = emailSendingService ?? throw new ArgumentNullException(nameof(emailSendingService));
        }

        [HttpGet]
        [Route("getTicketsOfUser")]
        public IHttpActionResult GetTicketsOfUser()
        {
            try
            {
                var user = _userContextService.GetCurrentUser();
                var tickets = _ticketService.GetTicketsOfUser(user.Id);
                return Ok(tickets);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("{getFreeTime}")]
        public IHttpActionResult GetFreeTime([FromBody]Models.BankIdAndDate bankIdAndDate)
        {
            string mes = string.Empty;
            try
            {
                var ticketTime = _ticketService.GetTicketTime();
                string date = bankIdAndDate.date.ToShortDateString();
                date = MakeCorrectDate(date);
                var bookedTime = _ticketService.GetBookedTime(date, bankIdAndDate.bankId);
                var freeTime = _ticketService.GetFreeTime(ticketTime, bookedTime);
                return Ok(freeTime);
            }
            catch (Exception ex)
            {
                mes = ex.Message;
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("getUserNameAndSurname")]
        public async Task<IHttpActionResult> GetUserNameAndSurname()
        {
            var user = _userContextService.GetCurrentUser();
            Guid userGuid = user.Id;
            var userProfile = await _userManager.GetProfileAsync(userGuid);

            string firstName = userProfile.FirstName;
            string lastName = userProfile.LastName;
            List<string> userName = new List<string>();
            userName.Add(firstName);
            userName.Add(lastName);

            return Ok(userName);
        }

        [HttpPost]
        [Route("sendTicketEmail")]
        public async Task SendTicketEmail(Models.DateAndTime dateAndTime)
        {
            string time = dateAndTime.time;
            string date = dateAndTime.date.ToShortDateString();

            var currentUser = _userContextService.GetCurrentUser();
            var user = await _userManager.FindByIdAsync(currentUser.IdentityId);

            Guid userGuid = currentUser.Id;
            var userProfile = await _userManager.GetProfileAsync(userGuid);

            string firstName = userProfile.FirstName;
            string lastName = userProfile.LastName;
            List<string> userName = new List<string>();
            userName.Add(firstName);
            userName.Add(lastName);

            var message = new MailMessage("from@email.com", user.Email)
            {
                 Body = $"Hi, {userName[0]} {userName[1]}! You have sucessfully ordered a ticket to <b>{date}</b> at <b>{time}</b>. Thank you for using the best bankig system. Hope to see you again!",
               // Body = $"<table class =\"ticket\" border=\"0\" cellpadding=\"4\" cellspacing=\"4\" style=\"width:300pt; margin: auto; \"><tr><td height = \"3\" style = \"border: 1pt solid #000000;\" align = \"left\" >< b > Date:</ b ></ td >< td height = \"3\" style = \"border: 1pt solid #000000;\" align = \"left\" >{date}</ td ></ tr >< tr >< td height = \"3\" style = \"border: 1pt solid #000000;\" align = \"left\" >< b > Time:</ b ></ td >< td height = \"3\" style = \"border: 1pt solid #000000;\" align = \"left\" >{time}</ td ></ tr >< tr >< td height = \"3\" style = \"border: 1pt solid #000000;\" align = \"left\" >< b > Name:</ b ></ td >< td height = \"3\" style = \"border: 1pt solid #000000;\" align = \"left\" >{userName[0]}</ td ></ tr >< tr >< td height = \"3\" style = \"border: 1pt solid #000000;\" align = \"left\" >< b > Surname:</ b ></ td >< td height = \"3\" style = \"border: 1pt solid #000000;\" align = \"left\" >{userName[1]}</ td ></ tr ></ table > ",
                Subject = "Ticket to bank",
                IsBodyHtml = true
            };
            await _emailSendingService.SendAsync(message);
        }

        /// <summary>
        /// Seeds a database with currency exchange tickets.
        /// </summary>
        /// <param name="ticket">A ticket to be seeded to datsbase.</param>
        /// <returns>A result of a seeding a database.</returns>
        [HttpPost]
        [Route("ticketCreation")]
        public async Task<IHttpActionResult> CreateTicket(Models.TicketInfo ticket)
        {
            var user = _userContextService.GetCurrentUser();
            string date = ticket.Date.ToShortDateString();
            string time = ticket.Time;
            date = MakeCorrectDate(date);

            CurrencyExchangeTicket t = new CurrencyExchangeTicket
            {
                BankId = ticket.BankId,
                Date = date,
                Time = time,
                ClientGuid = user.Id
            };

            try
            {
                await _ticketService.SaveTicketAsync(t);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok();
        }

        /// <summary>
        /// Seeds a database with  possible time to visit a bank.
        /// </summary>
        /// <returns>A result of a seeding a database.</returns>
        [HttpPost]
        [Route("seedTicketTime")]
        public async Task<IHttpActionResult> SeedTicketTime()
        {
            List<string> times = new List<string>();
            string t;
            for (int i = 9; i < 17; i++)
            {
                for (int j = 0; j < 45; j += 15)
                {
                    if (j == 0)
                    {
                        t = i.ToString() + ":" + j.ToString() + "0";
                    }
                    else
                    {
                        t = i.ToString() + ":" + j.ToString();
                    }

                    times.Add(t);
                }
            }

            foreach (var ti in times)
            {
                TicketTime ticketTime = new TicketTime
                {
                    Time = ti
                };

                await _ticketService.SaveTicketTimeAsync(ticketTime);
            }

            return Ok();
        }

        private async Task<IEnumerable<string>> GetUserNameSurname()
        {
            var user = _userContextService.GetCurrentUser();
            Guid userGuid = user.Id;
            var userProfile = await _userManager.GetProfileAsync(userGuid);

            string firstName = userProfile.FirstName;
            string lastName = userProfile.LastName;
            List<string> userName = new List<string>();
            userName.Add(firstName);
            userName.Add(lastName);

            return userName;
        }

        private string MakeCorrectDate(string date)
        {
            string day = date.Substring(0, 2);
            int dayInt = Convert.ToInt32(day);
            dayInt = dayInt + 1;
            string monthYear = date.Substring(3, 7);
            date = dayInt.ToString() + "." + monthYear;

            return date;
        }
    }
}