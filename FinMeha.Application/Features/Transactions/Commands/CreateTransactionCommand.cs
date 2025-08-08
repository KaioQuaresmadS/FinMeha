using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinMeha.Domain.Entities.Enums;

namespace FinMeha.Application.Features.Transactions.Commands
{
    public record CreateTransactionCommand (decimal Amount, string Description, TransactionType Type) : IRequest<Guid>; //Irequest<T> indica que espera um int como retorno
    
}
