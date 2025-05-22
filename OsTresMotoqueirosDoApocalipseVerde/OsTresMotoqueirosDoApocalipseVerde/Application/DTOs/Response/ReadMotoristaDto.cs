using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class ReadMotoristaDto
    {
        public int IdMotorista { get; set; }
        public Plano Plano { get; set; }
        public int? DadosId { get; set; }
    }
}
