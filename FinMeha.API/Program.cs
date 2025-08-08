using MediatR;
using FinMeha.Application;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using static System.Net.Mime.MediaTypeNames;
using FluentValidation.AspNetCore;
using FinMeha.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => {
    // A linha abaixo procura por todos os Handlers e Commands
    // no projeto onde a classe 'AssemblyReference' está.
    cfg.RegisterServicesFromAssembly(typeof(FinMeha.Application.AssemblyReference).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(FinMeha.Application.AssemblyReference).Assembly);
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

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
