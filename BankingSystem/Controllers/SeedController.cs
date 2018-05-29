#if DEBUG
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using BankingSystem.Data.Access.Context;
using BankingSystem.Identity;
using BankingSystem.Services.Identity;
using BankingSystem.Services.Security;
using Microsoft.AspNet.Identity;
using Swashbuckle.Swagger.Annotations;

namespace BankingSystem.Controllers
{
    /// <summary>
    /// Represents controller that is responsible for seeding and unseeding a database.
    /// </summary>
    //[Authorize(Roles = GlobalInfo.Admin)]
    [RoutePrefix("api/v1/database")]
    public class SeedController : ApiController
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationIdentityDbContext _identityDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeedController"/> class.
        /// </summary>
        /// <param name="appDbContext">Instance of <see cref="ApplicationDbContext"/>.</param>
        /// <param name="userManager">Instance of <see cref="ApplicationUserManager"/>.</param>
        /// <param name="identityDbContext">Instance of <see cref="ApplicationIdentityDbContext"/>.</param>
        public SeedController(
            ApplicationDbContext appDbContext,
            ApplicationUserManager userManager,
            ApplicationIdentityDbContext identityDbContext)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _identityDbContext = identityDbContext ?? throw new ArgumentNullException(nameof(identityDbContext));
        }

        /// <summary>
        /// Seeds a database with clients.
        /// </summary>
        /// <param name="amount">The amount of clients to seed.</param>
        /// <returns>Status of database seeding.</returns>
        /// <remarks>By default, an amount is 60.</remarks>
        // POST /api/v1/database/seed/candidates?amount=60
        [HttpPost]
        [Route("seed/clients")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, Description = "Parameter values are not correct")]
        public IHttpActionResult SeedCLients([FromUri] int amount = 60)
        {
            try
            {
                if (amount < 1)
                {
                    return BadRequest($"{nameof(amount)} must be greater than 0.");
                }

                AddCandidates(amount);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok("Database was successfully seeded.");
        }

        [HttpPost]
        [Route("seed/banksOfWorkers")]
        public IHttpActionResult SeedBanksOfWorkers()
        {
            try
            {
                Data.Access.BankManagement.BankOfBankWorker belarusBankWorker = new Data.Access.BankManagement.BankOfBankWorker
                {
                    BankId = 1,
                    WorkerGuid = new Guid("4eabfa0e-118e-4131-8ec9-1dbd29f68d3a")
                };

                Data.Access.BankManagement.BankOfBankWorker BTABankWorker = new Data.Access.BankManagement.BankOfBankWorker
                {
                    BankId = 2,
                    WorkerGuid = new Guid("AF789597-4EA1-4478-85E7-5A7D03D47948")
                };

                _appDbContext.BankOfBankWorker.Add(belarusBankWorker);
                _appDbContext.BankOfBankWorker.Add(BTABankWorker);
                _appDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok("Banks of bank workers was successfully seeded.");
        }

        /// <summary>
        /// Removes clients from database.
        /// </summary>
        /// <returns>Status of database unseeding.</returns>
        [HttpDelete]
        [Route("unseed/clients")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Description = "Internal server error")]
        public IHttpActionResult UnseedClients()
        {
            try
            {
                CleanCandidates();
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        private void AddCandidates(int amount = 60)
        {
            var testCandidates = new Bogus.Faker<ApplicationUser>()
                .RuleFor(i => i.Email, f => f.Internet.ExampleEmail())
                .RuleFor(i => i.UserName, (f, i) => i.Email)
                .RuleFor(i => i.DomainId, Guid.NewGuid)
                .RuleFor(i => i.IsActive, true)
                .RuleFor(i => i.EmailConfirmed, true);

            var userProfiles = new Bogus.Faker<UserProfileInfo>()
                .RuleFor(i => i.FirstName, f => f.Name.FirstName())
                .RuleFor(i => i.LastName, f => f.Name.LastName())
                .Generate(amount);

            var candidateRole = _identityDbContext.Roles.FirstOrDefault(role =>
                role.Name.Equals(GlobalInfo.Client, StringComparison.Ordinal));

            int j = 0;
            foreach (var candidate in testCandidates.Generate(amount))
            {
                if (_userManager.CreateAsync(candidate, "1").Result == IdentityResult.Success)
                {
                    _userManager.AddToRoleAsync(candidate.Id, candidateRole.Name).Wait();
                    _userManager.AddProfileAsync(candidate.DomainId, userProfiles[j++]).Wait();
                }
            }

            _identityDbContext.SaveChanges();
            _appDbContext.SaveChanges();
        }

        private void CleanCandidates()
        {
            foreach (var user in _identityDbContext.Users
                .Where(u => u.Roles
                    .Any(r => r.RoleId.Equals(
                        _identityDbContext.Roles
                            .FirstOrDefault(s => s.Name
                                .Equals(GlobalInfo.Client, StringComparison.Ordinal)).Id, StringComparison.Ordinal))))
            {
                _identityDbContext.UserProfiles.Remove(_identityDbContext.UserProfiles.Find(user.Id));
                _identityDbContext.Users.Remove(user);
            }

            _identityDbContext.SaveChanges();
        }
    }
}
#endif