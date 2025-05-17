using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.IO;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreatedMotoristaRequest
    {
        public long IdMotorista { get; set; }
        public Plano Plano { get; set; }
        public long DadosId { get; set; }
    }
}
