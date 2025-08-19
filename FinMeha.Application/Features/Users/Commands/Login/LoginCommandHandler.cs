using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinMeha.Domain.Interfaces;
using MediatR;

namespace FinMeha.Application.Features.Users.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar o usuário pelo email (responsabilidade do repositório)

            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (existingUser == null)
            {

                throw new Exception("Email ou senha inválidos");
            }

            // 2: Verificar a senha (usando uma biblioteca como BCrypt)
            // O PasswordHash deve vir  da sua entidade User (existingUser.PasswordHash)

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, existingUser.PasswordHash);

            if (!isPasswordValid)
            {
                throw new Exception("Email ou senha inválidos.");
            }

            // 3. Gerar o token JWT (responsabilidade do serviço de token)

            var token = _tokenService.GenerateToken(existingUser);

            // 4. Retornar o token gerado
            return token;
        }
    }
}
