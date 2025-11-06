

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

        public CreateMotoristaResponse Motorista { get; set; }
        public CreateModeloResponse Modelo { get; set; }

        public CreateSetorResponse Setor { get; set; }

        public CreateSituacaoResponse situacao { get; set; }
    }
}
