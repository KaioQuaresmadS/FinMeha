using MediatR;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using FinMeha.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using static System.Net.Mime.MediaTypeNames;
using FluentValidation.AspNetCore;
using FinMeha.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;
using FinMeha.Infrastructure;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, //ValidateIssuer: Verifica se o token foi emitido pelo emissor esperado.
            ValidateAudience = true, //ValidateAudience: Verifica se o token se destina a esta aplicação
            ValidateLifetime = true, //ValidateLifetime: Verifica se o token não expirou
            ValidateIssuerSigningKey = true, // ValidateIssuerSigningKey: Verifica se a chave de assinatura é válida, garantindo que o token não foi adulterado.

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new
                    SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthentication();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices(builder.Configuration);

// 1. Registrar MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("FinMeha.Application")));

// 2. Registrar Validadores do FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.Load("FinMeha.Application"));

// 3. Registrar o Pipeline Behavior de Validação
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
