using BackendAPICrud.Authentication;
using BackendAPICrud.Data;
using BackendAPICrud.Estudantes;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

string? mysqlStringConection = Environment.GetEnvironmentVariable("stringConection");

builder.Services.AddDbContextPool<AppDBContext>(options =>
    options.UseMySql(mysqlStringConection, 
    ServerVersion.AutoDetect(mysqlStringConection
)));

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.AddRouterEstudantes();
app.AddRouterAdmin();

app.Run();