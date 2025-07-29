using FinMeha.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinMeha.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public string? Description { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public TransactionType Type { get; private set; }

        public Transaction(string description, decimal amount, TransactionType type, DateTime date)
        {
            
            if (amount <= 0)
            {
                throw new InvalidOperationException("Esse campo não pode ser menor ou igual a zero");
            }

            Description = description;
            this.Amount = amount;
            Type = type;
            Date = date;
        }
    }
}
