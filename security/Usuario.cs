using System;

namespace BackendAPICrud.security;

// tabela para verificar usuario.
public class Usuario
{
    public Guid id {get; set;}
    public string? Username {get; set;}
    public string? Password {get; set;}

    public Usuario(Guid Id, string username)
    {
        id = Id;
        Username = username;
    }
}
