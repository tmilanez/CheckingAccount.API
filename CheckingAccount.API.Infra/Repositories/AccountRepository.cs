using CheckingAccount.API.Domain.Model;
using CheckingAccount.API.Domain.Repositories;
using CheckingAccount.API.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Infra.Repositories
{    
    public class AccountRepository : IAccountRepository
    {
        private readonly CheckingAccountDbContext _checkingAccountDb;

        public AccountRepository(CheckingAccountDbContext checkingAccountDb) 
        {
            _checkingAccountDb = checkingAccountDb;
        }

        public async Task<Account> CreateAsync(Account account)
        {
            _checkingAccountDb.Add(account);
            await _checkingAccountDb.SaveChangesAsync();
            return account;
        }

        public async Task<Account> GetBalanceByIdAsync(string id)
        {
           var account = await _checkingAccountDb.Accounts.FindAsync(id);

           return account;
        }
    }
}
