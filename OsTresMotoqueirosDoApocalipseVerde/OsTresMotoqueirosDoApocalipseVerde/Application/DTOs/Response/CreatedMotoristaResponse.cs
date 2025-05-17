using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreatedMotoristaResponse
    {
        public long IdMotorista { get; set; }
        public Plano Plano { get; set; }
        public long DadosId { get; set; }
    }
}
