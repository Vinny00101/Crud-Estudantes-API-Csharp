using System;
using BackendAPICrud.Data;
using BackendAPICrud.security;
using BackendAPICrud.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace BackendAPICrud.Authentication;

public static class RouterUsuarioAdmin 
{
    private static IServiceProvider? _serviceProvider;

    public static void Configure(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }
    public static void AddRouterAdmin(this WebApplication app)
    {
        if (_serviceProvider == null)
        {
            throw new InvalidOperationException("Service provider não configurado. Certifique-se de chamar Configure() antes de AddRouterAdmin().");
        }

        var RouterAdmin = app.MapGroup("Admin");

        RouterAdmin.MapPost("registro", async ( AppDBContext context, RequestRecord.RegistroRequest request) => {
            try{
                var Usuario = new Registro(request.username, request.password);
                await context.Registros.AddAsync(Usuario);
                await context.SaveChangesAsync();
                return Results.Ok();
            }catch (Exception ex) {
                return Results.BadRequest(ex.Message);
            }
        });
        RouterAdmin.MapPost("login", async ( AppDBContext context, RequestRecord.LoginRequest request) => {
            var user = await context.Registros.AnyAsync(registro => registro.Username == request.username);
            if (!user)
            {
                return Results.Conflict("Usuario e senha nao existe");
            }
            using(var scope = _serviceProvider.CreateScope())
            {
                var TokenService = scope.ServiceProvider.GetRequiredService<TokenService>();
                var securityC = new securityCript();

                if (securityC.VerificarPassword(request.password, request.username, context))
                {
                    var token = TokenService.GerarToken(request.username);
                    return Results.Ok( new {token});
                }

                return Results.Unauthorized();
            }
        });
    }
}
