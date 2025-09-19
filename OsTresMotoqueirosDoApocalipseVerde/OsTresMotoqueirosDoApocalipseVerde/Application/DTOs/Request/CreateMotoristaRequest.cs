using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateMotoristaRequest
    {
        public CreateDadosRequest Dados {  get; set; }

        [EnumDataType(typeof(Plano))]
        public Plano Plano { get; set; }
    }
}
