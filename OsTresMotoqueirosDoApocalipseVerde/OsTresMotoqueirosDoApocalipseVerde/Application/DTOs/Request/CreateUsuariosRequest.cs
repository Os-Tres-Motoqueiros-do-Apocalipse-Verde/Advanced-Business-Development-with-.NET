﻿using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateUsuariosRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

    }
}
