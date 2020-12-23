using EfCoreSandbox.EF;

namespace EfCoreSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new DatabaseContextFactory();

            using (var context = factory.CreateDbContext(args))
            {

            }
        }
    }
}
