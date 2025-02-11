using System;
using BackendAPICrud.Tables;
using Microsoft.EntityFrameworkCore;

namespace BackendAPICrud.Data;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options): base(options)
    {
    }
    public DbSet<Estudante> Estudante {get; set;}

    public DbSet<Nota> Notas {get; set;}
    public DbSet<Registro> Registros {get; set;}
    // public DbSet<> Registros {get; set}
    // public DbSet<> Registros {get; set}
}
