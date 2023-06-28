using CheckingAccount.API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Domain.Repositories
{
    public interface IMovementRepository
    {
        Task<Movement> CreateAsync(Movement movement);

        Task<IEnumerable<Movement>> GetMovementAsync(string accountId, DateTime initialDate, DateTime finalDate);

        Task<IEnumerable<Movement>> GetMovementByOperationTypeAsync(string accountId, string operationType);
    }
}
