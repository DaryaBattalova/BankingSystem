using System.Threading.Tasks;
using BankingSystem.Identity;
using Microsoft.AspNet.Identity;

namespace BankingSystem.Services.Identity
{
    /// <summary>
    /// Represents an application user manager.
    /// </summary>
    public interface IApplicationUserManager
    {
        IIdentityMessageService SmsService { get; }

        Task<IdentityResult> CreateAsync(ApplicationUser applicationUser, string password);

        Task<ApplicationUser> FindByNameAsync(string userName);

        Task<IdentityResult> AddToRoleAsync(string userId, string role);
    }
}
