using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinMeha.Domain.Interfaces;
using FinMeha.Domain.Entities;
using System.ComponentModel;

namespace FinMeha.Application.Features.Transactions.Commands
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Guid>
    {
        private readonly ITransactionRepository _transactionRepository; // Exemplo de Dependência

        // Injeção de dependência via contrutor

        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            //Validações (antes ou com FluentValidation)
            if (request.Amount <= 0)
            {
                throw new ArgumentException("Amount must be grater than zero.");
            }

            var transaction = new Transaction(
                request.Description,
                request.Amount,
                request.Type,
                request.Date

            );

            await _transactionRepository.AddAsync(transaction);

            return transaction.Id;
        }
    }
}
