using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateMotoristaResponse
    {
        public int IdMotorista { get; set; }
        public Plano Plano { get; set; }
        public int DadosId { get; set; }
    }
}
