﻿namespace PlataformaMotora.Application.DTOs.Requests
{
    public class RegisterRequest
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}
