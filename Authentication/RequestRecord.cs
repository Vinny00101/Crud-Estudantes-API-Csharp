using System;

namespace BackendAPICrud.Authentication;

public static class RequestRecord
{
    public record RegistroRequest(string username, string password);
    public record LoginRequest(string username, string password);
}
