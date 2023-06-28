using AutoMapper;
using CheckingAccount.API.Application.DTO;
using CheckingAccount.API.Application.Services.Interfaces;
using CheckingAccount.API.Application.Services;
using CheckingAccount.API.Controllers;
using CheckingAccount.API.Domain.Model;
using CheckingAccount.API.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Moq;

namespace CheckingAccount.API.Test
{
    public class MovementTest
    {
        private readonly Mock<IMovementService> _movementService;

        private readonly MovementController _controller;

        public MovementTest()
        {
            _movementService = new Mock<IMovementService>();
            _controller = new MovementController(_movementService.Object);
        }

        [Fact]
        public async void CreateMovement_Success()
        {
            //Arrange
            Moq.Mock<IMovementService> movementService = new Moq.Mock<IMovementService>();
            
            var model = new MovementDTO();

            //Act
            var result = _controller.CreateMovementAsync(model);

            //Assert
            Assert.True(result != null);
        }

        [Fact]
        public void GetMovement_Success()
        {
            //Arrange
            Moq.Mock<IMovementService> movementService = new Moq.Mock<IMovementService>();

            //Act
            var result = _controller.GetMovement("3e82c4a2-b440-45bf-999b-25f18cfde754", DateTime.Now, DateTime.Now);

            //Assert
            Assert.True(result != null);
        }

        [Fact]
        public void GetMovementByOperationType_Success()
        {
            //Arrange
            Moq.Mock<IMovementService> movementService = new Moq.Mock<IMovementService>();

            //Act
            var result = _controller.GetMovementByOperationType("3e82c4a2-b440-45bf-999b-25f18cfde754", "Deposit");

            //Assert
            Assert.True(result != null);
        }


        [Fact]
        public async void Transfer_Success()
        {
            //Arrange
            Moq.Mock<IMovementService> movementService = new Moq.Mock<IMovementService>();

            //Act
            var result = _controller.Transfer("8806797d-8975-46fe-84d6-a9f7bbb5ae2e", "3e82c4a2-b440-45bf-999b-25f18cfde754", 300);

            //Assert
            Assert.True(result != null);
        }
    }
}
