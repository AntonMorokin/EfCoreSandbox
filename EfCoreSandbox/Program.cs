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
                    await InitializeAsync(context);
                }

                var accountInYena = context.Accounts
                    .Include(a => a.Owner)
                    .ThenInclude(c => c.Accounts)
                    .Where(a => a.CurrencyName == "JPY")
                    .FirstOrDefault();

                var clientByAccount = accountInYena?.Owner;

                if (clientByAccount != null)
                {
                    clientByAccount.FullName = "Jason";
                    await context.SaveChangesAsync();
                }
            }
        }

        private static async Task InitializeAsync(DatabaseContext context)
        {
            var firstClient = new Client
            {
                FullName = "Abc Def",
                Address = "Hawaii",
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
                Address = "Okinawa"
            };

            context.Clients.Add(firstClient);
            context.Clients.Add(secondClient);

            await context.SaveChangesAsync();
        }
    }
}
