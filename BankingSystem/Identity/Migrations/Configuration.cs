using System;
using System.Data.Entity.Migrations;
using BankingSystem.Services.Identity;
using BankingSystem.Services.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BankingSystem.Identity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationIdentityDbContext>
    {
        public Configuration()
        {
            ContextKey = "ApplicationIdentity";
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            MigrationsDirectory = @"Identity\Migrations";
            MigrationsNamespace = "BankingSystem.Identity.Migrations";
        }

        protected override void Seed(ApplicationIdentityDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context), context);
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

            userManager.PasswordValidator = new PasswordValidator()
            {
                RequireDigit = false,
                RequireLowercase = false,
                RequireNonLetterOrDigit = false,
                RequireUppercase = false,
                RequiredLength = 1
            };

            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            var adminRole = new ApplicationRole(GlobalInfo.Admin);
            var candidateRole = new ApplicationRole(GlobalInfo.Client);
            var managerRole = new ApplicationRole(GlobalInfo.BankWorker);

            if (!roleManager.RoleExistsAsync(GlobalInfo.Admin).Result)
            {
                roleManager.CreateAsync(adminRole).Wait();
                roleManager.CreateAsync(candidateRole).Wait();
                roleManager.CreateAsync(managerRole).Wait();
            }

            var admin = new ApplicationUser
            {
                Email = "admin@test.com",
                EmailConfirmed = true,
                UserName = "admin@test.com",
                DomainId = new Guid("B4B6780C-F3BE-4554-BFFD-BCFD18E93A94"),
                IsActive = true
            };
            if (userManager.CreateAsync(admin, "123").Result == IdentityResult.Success)
            {
                userManager.AddToRoleAsync(admin.Id, adminRole.Name).Wait();
                userManager.AddProfileAsync(admin.DomainId, new UserProfileInfo
                {
                    FirstName = "Admin",
                    LastName = "Admin"
                }).Wait();
            }

            var candidate = new ApplicationUser
            {
                Email = "client@test.com",
                EmailConfirmed = true,
                UserName = "client@test.com",
                DomainId = new Guid("14620737-9BDF-4B4D-B6B9-01CD1EBE69EB"),
                IsActive = true
            };
            if (userManager.CreateAsync(candidate, "123").Result == IdentityResult.Success)
            {
                userManager.AddToRoleAsync(candidate.Id, candidateRole.Name).Wait();
                userManager.AddProfileAsync(candidate.DomainId, new UserProfileInfo
                {
                    FirstName = "Petr",
                    LastName = "Sidorenko"
                }).Wait();
            }

            var manager = new ApplicationUser
            {
                Email = "BelarusBankWorker@test.com",
                EmailConfirmed = true,
                UserName = "BelarusBankWorker@test.com",
                DomainId = new Guid("4eabfa0e-118e-4131-8ec9-1dbd29f68d3a"),
                IsActive = true
            };
            if (userManager.CreateAsync(manager, "123").Result == IdentityResult.Success)
            {
                userManager.AddToRoleAsync(manager.Id, managerRole.Name).Wait();
                userManager.AddProfileAsync(manager.DomainId, new UserProfileInfo
                {
                    FirstName = "BelarusBank",
                    LastName = "BankWorker"
                }).Wait();
            }

            var BTABankWorker = new ApplicationUser
            {
                Email = "BTABankWorker@test.com",
                EmailConfirmed = true,
                UserName = "BTABankWorker@test.com",
                DomainId = new Guid("AF789597-4EA1-4478-85E7-5A7D03D47948"),
                IsActive = true
            };
            if (userManager.CreateAsync(BTABankWorker, "123").Result == IdentityResult.Success)
            {
                userManager.AddToRoleAsync(BTABankWorker.Id, managerRole.Name).Wait();
                userManager.AddProfileAsync(BTABankWorker.DomainId, new UserProfileInfo
                {
                    FirstName = "BTABank",
                    LastName = "BankWorker"
                }).Wait();
            }
        }
    }
}