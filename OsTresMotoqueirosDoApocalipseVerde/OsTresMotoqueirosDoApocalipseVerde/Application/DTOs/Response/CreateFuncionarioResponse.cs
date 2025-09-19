using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateFuncionarioResponse
    {
        public CreateDadosRequest Dados { get; set; }

        [EnumDataType(typeof(Cargo))]
        public Cargo Cargo { get; set; }

        public CreateFilialResponse Filial { get; set; }
    }
}
