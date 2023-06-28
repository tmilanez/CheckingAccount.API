using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Domain.Validations
{
    public class ValidationException : Exception
    {
        public ValidationException(string error) : base(error) { }

        public static void When(bool hasError, string message)
        {
            if (hasError)
            {
                throw new ValidationException(message);
            }
        }        
    }
}
