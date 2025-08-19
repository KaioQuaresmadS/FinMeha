using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;


namespace FinMeha.Application.Features.Users.Commands.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<string>; //(retorna o token como string)

}
