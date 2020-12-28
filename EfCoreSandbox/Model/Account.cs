using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCoreSandbox.Model
{
    internal class Account
    {
        [Required, Key]
        public int AccountId { get; private set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string CurrencyName { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public DateTime OpeningDate { get; set; }

        [Required]
        public AccountStatus Status { get; set; }

        [Required]
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public Client Owner { get; set; }
    }
}
