using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinMeha.Application.Features.Transactions.Commands
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.Amount)
                                .GreaterThan(0).WithMessage("O valor da transação deve ser maior que zero")

                                .PrecisionScale(10, 2, false).WithMessage("O valor deve ter no máximo 2 casas decimais e 10 dígitos totais.");
            RuleFor(x => x.Description)
                                .NotEmpty().WithMessage("A descrição não pode exceder 200 caracteres.")
                                .MaximumLength(200).WithMessage("A descrição não pode exceder 200 caracteres.");
            RuleFor(x => x.Type)
                            .IsInEnum().WithMessage("Tipo de transação inválido.");
                            
        }
    }
}
