using System;

namespace BackendAPICrud.Tables;
// table das materias
public class Nota
{
    public Guid Id {get; init;}
    public double Matematica {get; private set;}
    public double Portugues {get; private set;}
    public double Ingles {get; private set;}
    public Nota()
    {
    }
    public Nota(Guid id, double Mat, double Port, double Ing)
    {
        Id = id;
        Matematica = Mat;
        Portugues = Port;
        Ingles = Ing;
    }
}
