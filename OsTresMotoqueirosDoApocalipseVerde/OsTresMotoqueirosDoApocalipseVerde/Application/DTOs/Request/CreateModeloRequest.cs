﻿using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateModeloRequest
    {
        public string NomeModelo { get; set; }

        [EnumDataType(typeof(Frenagem))]
        public Frenagem Frenagem { get; set; }

        [EnumDataType(typeof(SistemaPartida))]
        public SistemaPartida SistemaPartida { get; set; }

        public int Tanque { get; set; }

        [EnumDataType(typeof(TipoCombustivel))]
        public TipoCombustivel TipoCombustivel { get; set; }

        public int Consumo { get; set; }
    }
}
