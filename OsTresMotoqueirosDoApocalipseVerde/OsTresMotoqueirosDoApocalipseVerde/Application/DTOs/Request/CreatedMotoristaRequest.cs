using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.IO;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreatedMotoristaRequest
    {
        public Guid IdMotorista { get; set; }
        public Plano Plano { get; set; }
        public Guid DadosId { get; set; }
    }
}
