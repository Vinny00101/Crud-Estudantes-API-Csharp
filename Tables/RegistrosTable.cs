using System;
using BackendAPICrud.security;

namespace BackendAPICrud.Tables;
// Registro de Admins no sistema
//
public class Registro
{
    public Guid id {get; init;}
    public string? Username {get; private set;}
    public string? Password {get; private set;}

    public Registro()
    {
    }
    public Registro(string username, string password)
    {
        id = Guid.NewGuid();
        Username = username;
        var securityC = new securityCript();
        var passwordHasher = securityC.CriptPassword(id, password, username);
        Password = passwordHasher;
    }
    public Registro(Guid Id, string username)
    {
        id = Id;
        Username = username;
    }
}
