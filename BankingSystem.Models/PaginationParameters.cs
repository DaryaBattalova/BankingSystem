using BankingSystem.Models.Validators;
using FluentValidation.Attributes;

namespace BankingSystem.Models
{
    [Validator(typeof(PaginationParametersValidator))]
    public class PaginationParameters
    {
        public PaginationParameters()
        {
            Page = 1;
            Amount = 25;
        }

        public int Page { get; set; }

        public int Amount { get; set; }
    }
}
