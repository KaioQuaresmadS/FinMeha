using FinMeha.Application.Features.Users.Commands.Login;
using FinMeha.Application.Features.Users.Commands.Register;
using FinMeha.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinMeha.API.Controllers;

//O controller apenas injeta a interface IMediator, que será usada para enviar comandos e consultas.
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ApplicationDbContext _context;

    public UsersController(IMediator mediator, ApplicationDbContext context)
    {
        _mediator = mediator;
        _context = context;
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
    [HttpGet("me")]
    [Authorize]
    public IActionResult GetMyEmail()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(userEmail)) 
        {
            return NotFound("E-mail não encontrado!");
        }

        return Ok(new {Email = userEmail});
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        try
        {
            var token = await _mediator.Send(command);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
