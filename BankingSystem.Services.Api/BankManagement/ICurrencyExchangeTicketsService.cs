using BankingSystem.Models.BankManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankingSystem.Services.BankManagement
{
    public interface ICurrencyExchangeTicketsService
    {
        Task SaveTicketAsync(CurrencyExchangeTicket currencyExchangeTicket);

        IEnumerable<CurrencyExchangeTicket> GetTickets();

        IEnumerable<CurrencyExchangeTicket> GetTicketsOfUser(Guid userId);

        IEnumerable<string> GetBookedTime(string date, int bankId);

        IEnumerable<string> GetTicketTime();

        Task SaveTicketTimeAsync(TicketTime ticketTime);

        IEnumerable<string> GetFreeTime(IEnumerable<string> allTime, IEnumerable<string> bookedTime);

        CurrencyExchangeTicket GetTicketByDateAndTime(string date, string time);

        IEnumerable<CurrencyExchangeTicket> GetTicketsByBankId(int bankId);

        IEnumerable<CurrencyExchangeTicket> GetTicketsByDateAndBankId(string date, int bankId);
    }
}
