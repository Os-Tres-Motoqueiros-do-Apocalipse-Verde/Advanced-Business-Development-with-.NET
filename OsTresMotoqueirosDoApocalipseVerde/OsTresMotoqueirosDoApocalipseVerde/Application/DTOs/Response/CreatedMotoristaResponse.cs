using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreatedMotoristaResponse
    {
        public Guid IdMotorista { get; set; }
        public Plano Plano { get; set; }
        public Guid DadosId { get; set; }
    }
}
