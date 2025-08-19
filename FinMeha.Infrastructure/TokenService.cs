using FinMeha.Domain.Entities;
using FinMeha.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinMeha.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly IConfiguration _configuration;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        // A chave deve ser criada apenas uma vez no construtor
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
    }
    public string GenerateToken(User user)
    {

        // 1. Definir as "claims" (informações que irão dentro do token)
        // Note o uso de 'new List<Claims>' para criar a coleção corretamente.
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName)
             // Você pode adicionar mais claims customizadas aqui se precisar
        };

        // 2. Credenciais de assinanatura
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        // 3. Descritor do token
        var tokenDescriptior = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7), // Token expira em 7 dias
            SigningCredentials = creds,
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]

        };

        // 4. Criar e serializar o token

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptior);

        return tokenHandler.WriteToken(token);
    }
}