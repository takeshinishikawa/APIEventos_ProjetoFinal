﻿namespace APIEventos.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string role);
    }
}
