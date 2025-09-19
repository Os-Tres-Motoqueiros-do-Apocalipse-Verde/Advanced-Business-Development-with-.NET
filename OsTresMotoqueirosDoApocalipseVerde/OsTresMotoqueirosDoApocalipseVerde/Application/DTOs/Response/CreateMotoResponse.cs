namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateMotoResponse
    {
        public string Placa { get; set; }
        public string Chassi { get; set; }
        public string Condicao { get; set; }
        public Geometry LocalizacaoMoto { get; set; }

        // chave estrangeira
        public CreateModeloResponse Modelo { get; set; }

        public long? MotoristaId { get; set; }
        public long? SetorId { get; set; }
        public long? Setor { get; set; }
    }
}
