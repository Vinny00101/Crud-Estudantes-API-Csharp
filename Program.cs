using System.Text;
using BackendAPICrud.Authentication;
using BackendAPICrud.Data;
using BackendAPICrud.Estudantes;
using BackendAPICrud.security;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

string? mysqlStringConection = Environment.GetEnvironmentVariable("stringConection");

builder.Services.AddDbContextPool<AppDBContext>(options =>
    options.UseMySql(mysqlStringConection, 
    ServerVersion.AutoDetect(mysqlStringConection
)));


builder.Services.AddSingleton<TokenService>();

var Key = Environment.GetEnvironmentVariable("Key");
var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key!));

builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        
        ValidIssuer = Environment.GetEnvironmentVariable("Issuer"),
        ValidAudience = Environment.GetEnvironmentVariable("Audience"),
        IssuerSigningKey = SecurityKey
    };
});
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

RouterUsuarioAdmin.Configure(app.Services);
app.UseHttpsRedirection();
app.AddRouterEstudantes();
app.AddRouterAdmin();

app.Run();