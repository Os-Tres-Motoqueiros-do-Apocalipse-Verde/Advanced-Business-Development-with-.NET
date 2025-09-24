using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateSituacaoRequest
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }
    }
}
