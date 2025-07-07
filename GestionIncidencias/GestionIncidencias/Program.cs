using GestionIncidencias.Controllers;
using GestionIncidencias.DBContext;
using GestionIncidencias.DTOs;
using GestionIncidencias.Endpoints;
using GestionIncidencias.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<ServicioIncidencias>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{

    var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();
    if (!db.Database.CanConnect())
    {
        Console.WriteLine("No se pudo conectar a la base de datos.");
    }
    else
    {
        Console.WriteLine("Conectado");
    }

    
}

app.MapIncidenciasEndpoints();



app.Run();
