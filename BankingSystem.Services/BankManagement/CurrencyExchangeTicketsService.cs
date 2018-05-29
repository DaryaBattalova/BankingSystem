using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankingSystem.Data.Access.Context.Interfaces;
using BankingSystem.Models.BankManagement;

namespace BankingSystem.Services.BankManagement
{
    public class CurrencyExchangeTicketsService : ICurrencyExchangeTicketsService
    {
        private readonly ITicketContext _context;

        public CurrencyExchangeTicketsService(ITicketContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<CurrencyExchangeTicket> GetTickets()
        {
            var tickets = _context.Tickets;
            var result = Mapper.Map<IEnumerable<BankingSystem.Models.BankManagement.CurrencyExchangeTicket>>(tickets);
            return result.ToArray();
        }

        public IEnumerable<CurrencyExchangeTicket> GetTicketsOfUser(Guid userId)
        {
            var tickets = _context.Tickets.Where(t => t.ClientGuid == userId);
            var result = Mapper.Map<IEnumerable<BankingSystem.Models.BankManagement.CurrencyExchangeTicket>>(tickets);
            return result.ToArray();
        }

        public IEnumerable<string> GetFreeTime(IEnumerable<string> allTime, IEnumerable<string> bookedTime)
        {
            List<string> freeTime = new List<string>();

            return allTime.Except(bookedTime);
        }

        public IEnumerable<string> GetBookedTime(string date, int bankId)
        {
            var tickets = GetTicketsByDateAndBankId(date, bankId);
            List<string> time = new List<string>();
            foreach (var t in tickets)
            {
                time.Add(t.Time);
            }

            return time;
        }

        public IEnumerable<string> GetTicketTime()
        {
            var ticketTime = _context.TicketTime;
            List<string> time = new List<string>();

            foreach (var t in ticketTime)
            {
                time.Add(t.Time);
            }

            return time;
        }

        public Task SaveTicketAsync(CurrencyExchangeTicket currencyExchangeTicket)
        {
            _context.Tickets.Add(Mapper.Map<Data.Access.BankManagement.CurrencyExchangeTicket>(currencyExchangeTicket));
            return _context.SaveChangesAsync();
        }

        public Task SaveTicketTimeAsync(TicketTime ticketTime)
        {
            _context.TicketTime.Add(Mapper.Map<Data.Access.BankManagement.TicketTime>(ticketTime));
            return _context.SaveChangesAsync();
        }

        public CurrencyExchangeTicket GetTicketByDateAndTime(string date, string time)
        {
            string mes = string.Empty;
            CurrencyExchangeTicket result = null;

            try
            {
                var ticket = _context.Tickets.Where(r => r.Date == date && r.Time == time);
                Data.Access.BankManagement.CurrencyExchangeTicket[] arr = ticket.ToArray();
                result = Mapper.Map<BankingSystem.Models.BankManagement.CurrencyExchangeTicket>(arr[0]);
            }
            catch (Exception ex)
            {
               mes = ex.Message;
            }

            return result;
        }

        public IEnumerable<CurrencyExchangeTicket> GetTicketsByBankId(int bankId)
        {
            var tickets = _context.Tickets.Where(k => k.BankId == bankId);
            var result = Mapper.Map<IEnumerable<BankingSystem.Models.BankManagement.CurrencyExchangeTicket>>(tickets);
            return result.ToArray();
        }

        public IEnumerable<CurrencyExchangeTicket> GetTicketsByDateAndBankId(string date, int bankId)
        {
            var tickets = _context.Tickets.Where(d => d.Date == date && d.BankId == bankId);
            var result = Mapper.Map<IEnumerable<BankingSystem.Models.BankManagement.CurrencyExchangeTicket>>(tickets);
            return result.ToArray();
        }
    }
}
