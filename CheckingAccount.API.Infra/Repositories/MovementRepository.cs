﻿using CheckingAccount.API.Domain.Model;
using CheckingAccount.API.Domain.Repositories;
using CheckingAccount.API.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Infra.Repositories
{
    public class MovementRepository : IMovementRepository
    {
        private readonly CheckingAccountDbContext _checkingAccountDb;

        public MovementRepository(CheckingAccountDbContext checkingAccountDb)
        {
            _checkingAccountDb = checkingAccountDb;
        }

        public async Task<Movement> CreateAsync(Movement movement)
        {
            _checkingAccountDb.Add(movement);
            await _checkingAccountDb.SaveChangesAsync();
            return movement;
        }

        public async Task<IEnumerable<Movement>> GetMovementAsync(string accountId, DateTime initialDate, DateTime finalDate)
        {
            List<Movement> movement = new();
            movement = await _checkingAccountDb.Movements.Where(x => x.Id.Equals(accountId) && x.Date >= initialDate && x.Date <= finalDate).ToListAsync();

            return movement;
        }

        public async Task<IEnumerable<Movement>> GetMovementByOperationTypeAsync(string accountId, string type)
        {
            List<Movement> movement = new();

            movement = await _checkingAccountDb.Movements.Where(x => x.AccountId.Equals(accountId) && x.Type.Equals(type)).ToListAsync();

            return movement;
        }
    }
}
