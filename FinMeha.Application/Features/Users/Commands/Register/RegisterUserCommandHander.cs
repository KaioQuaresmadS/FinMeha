using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinMeha.Domain.Entities;
using FinMeha.Domain.Interfaces;
using MediatR;
using Org.BouncyCastle.Crypto.Generators;

namespace FinMeha.Application.Features.Users.Commands.Register;

public class RegisterUserCommandHander : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    public RegisterUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // 1. Verifica se o usuário já existe

        var existingUser = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
        if(existingUser is not null)
        {
            //Lançar uma exceção específica que pode ser trada na camada de API

            throw new Exception("Favor informar um email já cadastrado");
        }

        // 2. Hash da senha(será implementado corretamente mais tarde)
        //Por enquanto, usaremos uma implementação placeholder. Nunca armazene senhas em texto plano
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // 3.Criar a entidade User
        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash
            );
        // 4. Adicionar o usuário ao repositório
        await _userRepository.AddAsync(user, cancellationToken);
        // 5. Retornar o ID do novo usuário
        return user.Id;
    }
}
