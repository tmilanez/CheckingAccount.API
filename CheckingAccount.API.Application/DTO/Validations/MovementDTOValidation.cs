using CheckingAccount.API.Domain.Model;
using CheckingAccount.API.Domain.Repositories;
using CheckingAccount.API.Domain.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Application.DTO.Validations
{
    public class MovementDTOValidation : AbstractValidator<MovementDTO>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMovementRepository _movementRepository;

        public MovementDTOValidation(IAccountRepository accountRepository, IMovementRepository movementRepository)
        {
            _accountRepository = accountRepository;
            _movementRepository = movementRepository;

            RuleSet(MovementValidation.Deposit, () =>
            {
                ValidateExistingAccount();

                new MovementDTOValidation();
            });

            RuleSet(MovementValidation.Withdraw, () =>
            {
                ValidateExistingAccount();
                ValidateBalance();

                new MovementDTOValidation();
            });

            RuleSet(MovementValidation.Transfer, () =>
            {
                ValidateExistingAccount();
                ValidateBalance();
            });

        }

        private void ValidateBalance()
        {
            RuleFor(movement => movement.AccountId).MustAsync(async (movement, id, context, cancellationToken) =>
            {
                var account = await _accountRepository.GetBalanceByIdAsync(id);

                context.RootContextData.Add("Account", account);

                if (account.Balance + account.Limit > movement.Amount)
                    return true;

                return false;
            }).WithMessage("The informed account do not have enough funds!");
        }

        private void ValidateExistingAccount()
        {
            RuleFor(movement => movement.AccountId).MustAsync(async (movement, id, context, cancellationToken) =>
            {
                var account = await _accountRepository.GetBalanceByIdAsync(id);

                if (account is not null)
                {
                    context.RootContextData.Add("Account", account);
                    return true;
                }
                return false;
            }).WithMessage("The informed account was not found!");
        }

        public MovementDTOValidation()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty()
                .NotEmpty()
                .WithMessage("The account id must be informed!");

            RuleFor(x => x.Amount)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .WithMessage("The amount must be informed!");

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("The description cannot be null!");

            RuleFor(x => x.Type)
                .NotEmpty()
                .NotNull()
                .WithMessage("The operation type must be informed!");
        }
    }
}
