using CheckingAccount.API.Application.DTO;
using CheckingAccount.API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Application.Services.Interfaces
{
    public interface IMovementService
    {
        Task<ResultService<MovementDTO>> CreateAsync(MovementDTO account);

        Task<ResultService<IEnumerable<MovementDTO>>> GetMovement(string accountId, DateTime initialDate, DateTime finalDate);

        Task<ResultService<IEnumerable<MovementDTO>>> GetMovementByOperationTypeAsync(string accountId, string type);

        Task<ResultService> TransferAsync(string accountId, string destinationAccountId, decimal amount);
    }
}
