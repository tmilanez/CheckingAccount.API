using CheckingAccount.API.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Application.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ResultService<AccountDTO>> CreateAsync(AccountDTO accountDTO);

        Task<ResultService<AccountDTO>> GetBalanceAsync(string id);
    }
}
