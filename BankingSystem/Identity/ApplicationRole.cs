using Microsoft.AspNet.Identity.EntityFramework;

namespace BankingSystem.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string name)
            : base(name)
        {
        }
    }
}
