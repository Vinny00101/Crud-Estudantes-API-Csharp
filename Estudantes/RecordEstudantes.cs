using System;

namespace BackendAPICrud.Estudantes;

public static class RecordEstudantes
{
    public record AddEstudantesRequest(string Nome);
    public record UpdateEstudanteRequest(string Nome);
}