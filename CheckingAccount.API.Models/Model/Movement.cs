using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Domain.Model
{
    public class Movement
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }
}
