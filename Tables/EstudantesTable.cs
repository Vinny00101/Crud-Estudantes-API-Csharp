using System;

namespace BackendAPICrud.Tables;

public class Estudante
{
    public Guid id { get; init; }
    public string? Nome { get; private set;}
    public bool Ativo { get; private set; }

    public Estudante()
    {
        
    }
    public Estudante(string name )
    {
        Nome = name;
        id = Guid.NewGuid();
        Ativo = true;
    }

    public void AtualizarNome(string name){
        
        Nome = name;
    }
}
