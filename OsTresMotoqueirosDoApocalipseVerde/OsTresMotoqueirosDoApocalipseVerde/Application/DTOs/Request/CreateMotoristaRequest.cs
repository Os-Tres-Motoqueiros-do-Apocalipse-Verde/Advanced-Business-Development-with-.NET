using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateMotoristaRequest
    {
        [EnumDataType(typeof(Plano))]
        public Plano Plano { get; set; }

        public long DadosId { get; set; }
    }
}
