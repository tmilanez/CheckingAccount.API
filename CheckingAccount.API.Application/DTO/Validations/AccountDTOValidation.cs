using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Application.DTO.Validations
{
    public class AccountDTOValidation : AbstractValidator<AccountDTO>
    {
        public AccountDTOValidation() 
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage(("The client name cannot be null!"));

            RuleFor(x => x.Balance) 
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("The account balance cannot be null!");        
        }
    }
}
