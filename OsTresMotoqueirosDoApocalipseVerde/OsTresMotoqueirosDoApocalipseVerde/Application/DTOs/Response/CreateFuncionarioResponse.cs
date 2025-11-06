using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateFuncionarioResponse
    {
        public long Id { get; set; }

        [EnumDataType(typeof(Cargo))]
        public Cargo Cargo { get; set; }

        public CreateDadosResponse Dados { get; set; }

        public long FilialId { get; set; }
    }
}
