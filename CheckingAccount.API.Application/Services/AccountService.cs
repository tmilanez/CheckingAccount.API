using AutoMapper;
using CheckingAccount.API.Application.DTO;
using CheckingAccount.API.Application.DTO.Validations;
using CheckingAccount.API.Application.Services.Interfaces;
using CheckingAccount.API.Domain.Model;
using CheckingAccount.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<AccountDTO>> CreateAsync(AccountDTO accountDTO)
        {
            if (accountDTO == null)
                return ResultService.Fail<AccountDTO>("The Account parameters must be informed!");

            var result = new AccountDTOValidation().Validate(accountDTO);
            if (!result.IsValid)
                return ResultService.RequestError<AccountDTO>("An error has ocurred when validating the request data.", result);

            accountDTO.Id = Guid.NewGuid().ToString();

            var account = _mapper.Map<Account>(accountDTO);

            var data = await _accountRepository.CreateAsync(account);

            return ResultService.Ok<AccountDTO>(_mapper.Map<AccountDTO>(data));
        }

        public async Task<ResultService<AccountDTO>> GetBalanceAsync(string id)
        {
            if (id == null)
                return ResultService.Fail<AccountDTO>("The account id must be informed!");

            var balance = await _accountRepository.GetBalanceByIdAsync(id);

            if (balance == null)
                return ResultService.Fail<AccountDTO>("The account could not be found by the informed id.");           

            return ResultService.Ok(_mapper.Map<AccountDTO>(balance));
        }
    }
}
