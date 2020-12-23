using EfCoreSandbox.EF;
using Microsoft.Extensions.Configuration;

namespace EfCoreSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config["connectionStrings:postgreSql"];

            using (var context = new DatabaseContext(connectionString))
            {

            }
        }
    }
}
