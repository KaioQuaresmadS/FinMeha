using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FinMeha.Application.Features.Users.Commands.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50).WithMessage("Primeiro nome não pode execeder 50 caracteres.");
        RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50).WithMessage("Último nome não pode execeder 50 caracteres");
        RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("Informe um email válido");
        RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Informe uma senha válida")
                .MinimumLength(8).WithMessage("A senha tem que ter no mínimo 8 caracteres");
    }
}
