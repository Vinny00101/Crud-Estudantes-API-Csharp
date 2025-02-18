using System;
using System.Threading.Tasks;
using BackendAPICrud.Data;
using BackendAPICrud.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackendAPICrud.security;

public class securityCript
{

    public string CriptPassword(Guid id, string password, string username)
    {
        var Hasher = new PasswordHasher<Registro>();
        var Usuario = new Registro(id, username);
        string? senhaCriptografada = Hasher.HashPassword(Usuario, password);
        return senhaCriptografada;
    }

    public bool VerificarPassword(string password, string username, AppDBContext context)
    {
        var Hasher = new PasswordHasher<Usuario>();
        var User = context.Registros
            .Where(user => user.Username == username)
            .Select(user => new {user.id, user.Username, user.Password})
            .FirstOrDefault()!;
        
        string passwordCript = User.Password!;
        var Usuario = new Usuario(User.id, username);
        var VerificaPassword = Hasher.VerifyHashedPassword(Usuario, passwordCript, password);

        if (VerificaPassword == PasswordVerificationResult.Success)
        {
            return true;
        }
        return false;
    }
}
