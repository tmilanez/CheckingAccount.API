using CheckingAccount.API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> CreateAsync(Account account);
        Task<Account> GetBalanceByIdAsync(string id);
    }
}
