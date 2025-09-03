using FinMeha.Application.Common.Behaviors;
using FinMeha.Domain.Interfaces;
using FinMeha.Infrastructure;
using FinMeha.Infrastructure.Persistence;
using FinMeha.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuração de CORS ✅ (ANTES de Build())

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",  policy =>
        {
            policy.WithOrigins(
                "http://localhost:4200",
                "http://localhost:50107",    // Desenvolvimento
                "https://seu-frontend.azurewebsites.net"  // Produção
                )
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// Autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Serviços da aplicação
builder.Services.AddControllers(); // ✅ Apenas UMA vez
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddScoped<ITokenService, TokenService>();

// MediatR e Validação
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.Load("FinMeha.Application")));
builder.Services.AddValidatorsFromAssembly(Assembly.Load("FinMeha.Application"));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();

// ✅ Configuração do pipeline APÓS Build (apenas middleware)

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend"); // ✅ USO do CORS (não configuração)
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
