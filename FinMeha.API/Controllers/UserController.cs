using FinMeha.Application.Features.Users.Commands.Register;
using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FinMeha.API.Controllers;

//O controller apenas injeta a interface IMediator, que será usada para enviar comandos e consultas.
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        try
        {
            var userId = await _mediator.Send(command);
            return Ok(new { UserId = userId });
        }
        catch (FluentValidation.ValidationException ex)
        {
            return BadRequest(ex.Errors);
        }
        catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("profile")]
    [Authorize] //Esse endpoint agora está protegido
    public IActionResult GetUserProfile()
    {
        // lógica para retornar o perfil do usuário autenticado
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        return Ok(new { Email = userEmail });
    }
}
