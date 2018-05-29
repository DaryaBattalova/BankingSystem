using System.Threading.Tasks;

namespace BankingSystem.Services.Security
{
    public interface IUserContextService
    {
        IUserContext GetCurrentUser();

        Task<IUserContext> GetCurrentUserAsync();
    }
}
