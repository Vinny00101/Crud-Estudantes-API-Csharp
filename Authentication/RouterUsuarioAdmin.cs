using System;
using BackendAPICrud.Data;
using BackendAPICrud.security;
using BackendAPICrud.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BackendAPICrud.Authentication;

public static class RouterUsuarioAdmin
{
    public static void AddRouterAdmin(this WebApplication app)
    {
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
            try{
                if (user)
                {
                    var securityC = new securityCript();
                    if (securityC.VerificarPassword(request.password, request.username, context))
                    {
                        return Results.Ok("Login bem efetuado");
                    }
                    return Results.Conflict("Senha incorreta");
                }
                return Results.Conflict("Usuario nao existe");
            }catch (Exception ex){
                return Results.BadRequest(ex.Message);
            }

        });
    }
}
