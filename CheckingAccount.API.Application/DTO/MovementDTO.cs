using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Application.DTO
{
    public class MovementDTO
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }
}
