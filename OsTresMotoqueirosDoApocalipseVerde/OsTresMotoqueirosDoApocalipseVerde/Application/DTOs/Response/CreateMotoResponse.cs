using NetTopologySuite.Geometries;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateMotoResponse
    {
        public long Id { get; set; }
        public string Placa { get; set; }
        public string Chassi { get; set; }
        public string Condicao { get; set; }
        public string LocalizacaoMoto { get; set; }

        // chave estrangeira
        public CreateModeloResponse Modelo { get; set; }

        public long MotoristaId { get; set; }
        public long SetorId { get; set; }

        public long SituacaoId { get; set; }
    }
}
