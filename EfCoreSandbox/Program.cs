using EfCoreSandbox.EF;
using EfCoreSandbox.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EfCoreSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessAsync(args).Wait();
        }

        private static async Task ProcessAsync(string[] args)
        {
            var factory = new DatabaseContextFactory();

            using (var context = factory.CreateDbContext(args))
            {
                if (args.Any(a => a == "init"))
                {
                    Initialize(context);
                }

                ChangeFullNameOfClientWithYenAccount(context);

                await SetClientBirthDates(context);

                await context.SaveChangesAsync();
            }
        }

        private static void Initialize(DatabaseContext context)
        {
            var firstClient = new Client
            {
                FullName = "Abc Def",
                Address = "Hawaii",
                DateOfBirth = DateTime.Now.AddYears(-35),
                Accounts = new System.Collections.Generic.List<Account>
                    {
                        new Account
                        {
                            AccountNumber = "1234",
                            CurrencyName = "USD",
                            Balance = 456321.32m,
                            Status = AccountStatus.Active
                        },
                        new Account
                        {
                            AccountNumber = "5678",
                            CurrencyName = "JPY",
                            Balance = 987456,
                            Status = AccountStatus.Locked,
                            OpeningDate = DateTime.Now.AddYears(-3)
                        }
                    }
            };

            var secondClient = new Client
            {
                FullName = "Ghi Jkl",
                Address = "Okinawa",
                DateOfBirth = DateTime.Now.AddYears(-21).AddMonths(6).AddDays(-18),
            };

            context.Clients.Add(firstClient);
            context.Clients.Add(secondClient);
        }

        private static void ChangeFullNameOfClientWithYenAccount(DatabaseContext context)
        {
            var accountInYen = context.Accounts
                                .Include(a => a.Owner)
                                .ThenInclude(c => c.Accounts)
                                .Where(a => a.CurrencyName == "JPY")
                                .FirstOrDefault();

            var clientByAccount = accountInYen?.Owner;

            if (clientByAccount != null)
            {
                clientByAccount.FullName = "Jason";
            }
        }

        private static async Task SetClientBirthDates(DatabaseContext context)
        {
            var r = new Random();
            int minAge = 21;
            int maxAge = 35;

            await context.Clients
                .Where(c => c.DateOfBirth == DateTime.MinValue)
                .LoadAsync();

            foreach (var client in context.Clients.Where(c => c.DateOfBirth == DateTime.MinValue))
            {
                client.DateOfBirth = DateTime.Now
                    .AddYears(-r.Next(minAge - 1, maxAge))
                    .AddDays(-r.Next(365))
                    .Date;
            }
        }
    }
}
