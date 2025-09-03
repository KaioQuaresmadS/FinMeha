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
[Route("api/[controller]")] //Adiciona rota base
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
            var result = await _mediator.Send(command);
            return Ok(new { 
                
                UserId = result,
                Message = "Usuário registrado com sucesso"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        try
        {
            var token = await _mediator.Send(command);
            return Ok(new {
                Token = token,
                Message = "Login realizado com sucesso!",
                ExpiresIn = "1 hora" // Ajuste conforme sua configuração JWT
            });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpGet("profile")]
    [Authorize] //Esse endpoint agora está protegido
    public async Task<IActionResult> GetUserProfile()
    {
        try
        {
            // lógica para retornar o perfil do usuário autenticado
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //Buscar informações completas do usuário no banco
            var user = await _context.Users
                            .FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });


            return Ok(new
            {
                user.Id,
                user.Email,
                user.FirstName,
                user.CreatedAt
            
            });
        }

        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar perfil" });
        }

    }
    [HttpGet("me")]
    [Authorize]
    public IActionResult GetMyEmail()
    {
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userEmail)) 
        {
            return NotFound(new { message = "E-mail não encontrado!" });
        }

        return Ok(new {
            Email = userEmail,
            UserId = userId,
            Message = "Informações do usuário autenticado"
        });
    }
}
