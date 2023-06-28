using CheckingAccount.API.Domain.Validations;
using System.Reflection.Metadata;

namespace CheckingAccount.API.Domain.Model
{
    public class Account
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public decimal Limit { get; set; }

    }
}