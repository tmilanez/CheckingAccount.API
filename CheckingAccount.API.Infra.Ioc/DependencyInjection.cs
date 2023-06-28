using Autofac;
using AutoMapper;
using CheckingAccount.API.Application.DTO.Validations;
using CheckingAccount.API.Application.Mappings;
using CheckingAccount.API.Application.Services;
using CheckingAccount.API.Application.Services.Interfaces;
using CheckingAccount.API.Domain.Repositories;
using CheckingAccount.API.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IMovementRepository, MovementRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddAutoMapper(typeof(AccountProfile));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IMovementService, MovementService>();
            return services; 
        }
    }
}
