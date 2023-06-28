using AutoMapper;
using CheckingAccount.API.Application.DTO;
using CheckingAccount.API.Application.DTO.Validations;
using CheckingAccount.API.Application.Services.Interfaces;
using CheckingAccount.API.Domain.Model;
using CheckingAccount.API.Domain.Repositories;
using CheckingAccount.API.Domain.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Application.Services
{
    public class MovementService : IMovementService
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public MovementService(IMovementRepository movementRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _movementRepository = movementRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<MovementDTO>> CreateAsync(MovementDTO movementDTO)
        {
            if (movementDTO == null)
                return ResultService.Fail<MovementDTO>("The Account parameters must be informed!");

            var movement = _mapper.Map<Movement>(movementDTO);

            var account = await _accountRepository.GetBalanceByIdAsync(movement.AccountId);

            switch (movementDTO.Description.ToUpper())
            {
                case "DEPOSIT":
                    {
                        var result = new MovementDTOValidation().Validate(movementDTO, opt => opt.IncludeRuleSets(MovementValidation.Deposit));
                        if (!result.IsValid)
                            return ResultService.RequestError<MovementDTO>("An error has ocurred when validating the request data.", result);

                        var data = await _movementRepository.CreateAsync(new Movement
                        {
                            Id = Guid.NewGuid().ToString(),
                            AccountId = account.Id,
                            Description = "DEPOSIT",
                            Amount = movement.Amount,
                            Type = "CREDIT",
                            Date = DateTime.Now
                        });

                        await Credit(account, movement.Amount);

                        return ResultService.Ok<MovementDTO>(_mapper.Map<MovementDTO>(data));
                    }
                case "WITHDRAW":
                    {
                        var result = new MovementDTOValidation().Validate(movementDTO, opt => opt.IncludeRuleSets(MovementValidation.Withdraw));
                        if (!result.IsValid)
                            return ResultService.RequestError<MovementDTO>("An error has ocurred when validating the request data.", result);

                        await Debit(account, movement.Amount);

                        var data = await _movementRepository.CreateAsync(new Movement
                        {
                            Id = Guid.NewGuid().ToString(),
                            AccountId = account.Id,
                            Description = "WITHDRAW",
                            Amount = movement.Amount,
                            Type = "DEBIT",
                            Date = DateTime.Now
                        });

                        return ResultService.Ok<MovementDTO>(_mapper.Map<MovementDTO>(data));
                    }
                default:
                    return ResultService.Fail<MovementDTO>("The Account parameters must be informed!");
            }
        }

        public async Task<ResultService<IEnumerable<MovementDTO>>> GetMovement(string accountId, DateTime initialDate, DateTime finalDate)
        {
            if (accountId == null)
                return ResultService.Fail<IEnumerable<MovementDTO>>("The account id must be informed!");

            var movement = await _movementRepository.GetMovementAsync(accountId, initialDate, finalDate);

            if (!movement.Any())
                return ResultService.Fail<IEnumerable<MovementDTO>>("No transactions were found for the specified period.");

            return ResultService.Ok(_mapper.Map<IEnumerable<MovementDTO>>(movement));
        }

        public async Task<ResultService<IEnumerable<MovementDTO>>> GetMovementByOperationTypeAsync(string accountId, string type)
        {
            if (accountId == null || type == null)
                return ResultService.Fail<IEnumerable<MovementDTO>>("The account id must be informed!");
            
            var operationType = type.ToUpper();

            var movement = await _movementRepository.GetMovementByOperationTypeAsync(accountId, operationType);

            if (!movement.Any())
                return ResultService.Fail<IEnumerable<MovementDTO>>("No transactions were found for the specified operation type.");

            return ResultService.Ok(_mapper.Map<IEnumerable<MovementDTO>>(movement));
        }

        public async Task<ResultService> TransferAsync(string accountId, string destinationAccountId, decimal amount)
        {
            try
            {
                var account = await _accountRepository.GetBalanceByIdAsync(accountId);
                var destinationAccount = await _accountRepository.GetBalanceByIdAsync(accountId);

                if (account == null)
                    return ResultService.Fail("Account could not be found by the informed Id.");

                if (destinationAccount == null)
                    return ResultService.Fail("Destination account could not be found by the informed Id.");

                await Debit(account, amount);

                await _movementRepository.CreateAsync(new Movement
                {
                    Id = Guid.NewGuid().ToString(),
                    AccountId = accountId,
                    Description = "WITHDRAW",
                    Amount = amount,
                    Type = "DEBIT",
                    Date = DateTime.Now
                });


                await _movementRepository.CreateAsync(new Movement
                {
                    Id = Guid.NewGuid().ToString(),
                    AccountId = destinationAccountId,
                    Description = "DEPOSIT",
                    Amount = amount,
                    Type = "CREDIT",
                    Date = DateTime.Now
                });

                await Credit(destinationAccount, amount);

                return ResultService.Ok("Successfull transfer");
            }
            catch(Exception) 
            {
                return ResultService.Fail("An error has ocurred when validating the request data.");
            }
        }

        private async Task Credit(Account account, decimal amount)
        {
            account.Balance += amount;
            await UpdateBalance(account);
        }

        private async Task Debit(Account account, decimal amount)
        {
            if (account.Balance < amount)
            {
                var funds = account.Balance + account.Limit;
                if(funds < amount)
                    throw new Exception("Insuficient funds.");

                var principalAmount = account.Balance;
                var remaingAmount = amount - principalAmount;

                account.Balance = 0;
                account.Limit -= remaingAmount;                
            }
            else
                account.Balance -= amount;

            await UpdateBalance(account);
        }

        private Task UpdateBalance(Account account)
        {
            var result = _accountRepository.UpdateBalance(account);
            return result;
        }
    }
}
