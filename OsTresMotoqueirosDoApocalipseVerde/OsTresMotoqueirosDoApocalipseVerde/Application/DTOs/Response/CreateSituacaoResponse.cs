using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateSituacaoResponse
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }
    }
}
