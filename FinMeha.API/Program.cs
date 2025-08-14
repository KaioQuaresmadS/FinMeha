using MediatR;
using FinMeha.Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using static System.Net.Mime.MediaTypeNames;
using FluentValidation.AspNetCore;
using FinMeha.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 1. Registrar MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServices.FromAssembly(Assembly.Load("FinMeha.Application")));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
