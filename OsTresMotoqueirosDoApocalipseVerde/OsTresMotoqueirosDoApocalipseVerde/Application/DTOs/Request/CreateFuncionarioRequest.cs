using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateFuncionarioRequest
    {
        public CreateDadosRequest Dados { get; set; }

        [EnumDataType(typeof(Cargo))]
        public Cargo Cargo { get; set; }

        public long? FilialId { get; set; }

    }
}
