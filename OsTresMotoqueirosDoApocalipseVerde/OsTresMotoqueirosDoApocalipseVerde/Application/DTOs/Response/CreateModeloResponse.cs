using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateModeloResponse
    {
        public long Id { get; set; }
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
