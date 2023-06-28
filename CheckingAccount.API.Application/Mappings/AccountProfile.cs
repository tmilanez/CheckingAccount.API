using AutoMapper;
using CheckingAccount.API.Application.DTO;
using CheckingAccount.API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Application.Mappings
{
    public class AccountProfile : Profile
    {
        public AccountProfile() 
        { 
            CreateMap<Account, AccountDTO>().ReverseMap();
        }
    }
}
