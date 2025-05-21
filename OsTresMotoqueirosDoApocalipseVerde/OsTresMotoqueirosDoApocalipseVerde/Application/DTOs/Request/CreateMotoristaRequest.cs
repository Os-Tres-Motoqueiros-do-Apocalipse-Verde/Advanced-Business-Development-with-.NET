using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.IO;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateMotoristaRequest
    {
        public Plano Plano { get; set; }
        public int DadosId { get; set; }
    }
}
