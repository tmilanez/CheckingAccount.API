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

        public Account(string name, decimal balance, decimal limit)
        {
            Name = name;
            Balance = balance;
            Limit = limit;
        }

        //private void Validation(string name, decimal balance)
        //{
        //    ValidationException.When(string.IsNullOrEmpty(name), "The client name cannot be null.");
        //    ValidationException.When(balance < 0, "The account balance cannot be null.");

        //    Name = name;
        //    Balance = balance; 
        //}
    }
}