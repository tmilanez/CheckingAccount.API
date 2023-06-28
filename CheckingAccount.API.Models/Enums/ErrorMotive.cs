using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Domain.Enums
{
    public enum ErrorMotive
    {
        InternalServerError,
        BadRequest,
        Conflict,
        NotFound,
        NoContent
    }
}
