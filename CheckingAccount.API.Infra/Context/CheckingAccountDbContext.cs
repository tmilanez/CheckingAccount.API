using CheckingAccount.API.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Infra.Context
{
    public class CheckingAccountDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "CheckingAccountDb");
        }
        //public CheckingAccountDbContext(DbContextOptions<CheckingAccountDbContext> options) : base(options)
        //{}

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Movement> Movements { get; set; }
    }
}
