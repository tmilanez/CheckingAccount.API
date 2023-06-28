using AutoMapper;
using CheckingAccount.API.Application.DTO;
using CheckingAccount.API.Application.DTO.Validations;
using CheckingAccount.API.Application.Services.Interfaces;
using CheckingAccount.API.Domain.Model;
using CheckingAccount.API.Domain.Repositories;
using CheckingAccount.API.Domain.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckingAccount.API.Application.Services
{
    public class MovementService : IMovementService
    {
        private readonly IMovementRepository _movementRepository;
        private readonly IMapper _mapper;

        public MovementService(IMovementRepository movementRepository, IMapper mapper)
        {
            _movementRepository = movementRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<MovementDTO>> CreateAsync(MovementDTO movementDTO)
        {
            if (movementDTO == null)
                return ResultService.Fail<MovementDTO>("The Account parameters must be informed!");

            movementDTO.Id = Guid.NewGuid().ToString();

            var movement = _mapper.Map<Movement>(movementDTO);

            switch (movementDTO.Description)
            {
                case "Deposit":
                    {
                        var result = new MovementDTOValidation().Validate(movementDTO, opt => opt.IncludeRuleSets(MovementValidation.Deposit));
                        if (!result.IsValid)
                            return ResultService.RequestError<MovementDTO>("An error has ocurred when validating the request data.", result);
                        
                        var data = await _movementRepository.CreateAsync(movement);

                        return ResultService.Ok<MovementDTO>(_mapper.Map<MovementDTO>(data));
                    }
                case "Withdraw":
                    {
                        var result = new MovementDTOValidation().Validate(movementDTO, opt => opt.IncludeRuleSets(MovementValidation.Withdraw));
                        if (!result.IsValid)
                            return ResultService.RequestError<MovementDTO>("An error has ocurred when validating the request data.", result);

                        var data = await _movementRepository.CreateAsync(movement);

                        return ResultService.Ok<MovementDTO>(_mapper.Map<MovementDTO>(data));
                    }
                default:
                    return ResultService.Fail<MovementDTO>("The Account parameters must be informed!");
            }
        }

        Task<ResultService<IEnumerable<MovementDTO>>> IMovementService.GetMovement(string accountId, DateTime initialDate, DateTime finalDate)
        {
            throw new NotImplementedException();
        }

        Task<ResultService<IEnumerable<MovementDTO>>> IMovementService.GetMovementByOperationTypeAsync(string accountId, string type)
        {
            throw new NotImplementedException();
        }

        public async Task TransferAsync(string accountId, string destinationAccountId, decimal amount)
        {
            //var result = new MovementDTOValidation().Validate(, opt => opt.IncludeRuleSets(MovementValidation.Transfer));
            //if (!result.IsValid)
            //   throw new Exception("An error has ocurred when validating the request data.");



            return;
        }

        Task<ResultService> IMovementService.TransferAsync(string accountId, string destinationAccountId, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
