using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinMeha.Application.Features.Users.Commands.Register;

public  record RegisterUserCommand(string FirstName, string LastName, string Email, string Password) : IRequest<Guid>;
