using AutoMapper;
using CheckingAccount.API.Application.DTO;
using CheckingAccount.API.Application.Services;
using CheckingAccount.API.Application.Services.Interfaces;
using CheckingAccount.API.Controllers;
using CheckingAccount.API.Domain.Model;
using CheckingAccount.API.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CheckingAccount.API.Test
{
    public class AccountTest
    {
        private readonly Mock<IAccountService> _accountService;

        private readonly AccountController _controller;

        public AccountTest()
        {
            _accountService = new Mock<IAccountService>();
            _controller = new AccountController(_accountService.Object);
        }

        [Fact]
        public void GetBalance_Success()
        {

            var result = _controller.GetBalance("3e82c4a2-b440-45bf-999b-25f18cfde754");

            //Assert
            Assert.True(result != null);
        }

        [Fact]
        public void CreateAccount_Success()
        {
            //Arrange
            Moq.Mock<IAccountService> accountService = new Moq.Mock<IAccountService>();

            var model = new AccountDTO();

            //Act
            var result = _controller.Create(model);

            //Assert
            Assert.True(result != null);
        }
    }
}      
        