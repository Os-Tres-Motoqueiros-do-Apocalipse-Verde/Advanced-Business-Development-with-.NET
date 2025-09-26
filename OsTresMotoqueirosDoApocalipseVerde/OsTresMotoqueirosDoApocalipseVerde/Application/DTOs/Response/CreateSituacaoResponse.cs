using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateSituacaoResponse
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }
    }
}
