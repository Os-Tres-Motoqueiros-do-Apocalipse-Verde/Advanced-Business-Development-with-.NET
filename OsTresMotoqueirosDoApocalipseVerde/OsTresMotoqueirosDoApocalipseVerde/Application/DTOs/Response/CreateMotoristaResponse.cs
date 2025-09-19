using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateMotoristaResponse
    {
        public CreateDadosResponse Dados { get; set; }

        [EnumDataType(typeof(Plano))]
        public Plano Plano { get; set; }
    }
}
