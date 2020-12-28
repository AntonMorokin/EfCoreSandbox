using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EfCoreSandbox.Model
{
    internal sealed class Client
    {
        [Required, Key]
        public int ClientId { get; private set; }

        [Required]
        public string FullName { get; set; }

        public string Address { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
