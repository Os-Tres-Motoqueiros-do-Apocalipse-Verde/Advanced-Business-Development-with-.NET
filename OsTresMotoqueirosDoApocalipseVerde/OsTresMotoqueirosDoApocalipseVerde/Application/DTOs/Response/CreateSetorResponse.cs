namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreateSetorResponse
    {
        public long Id { get; set; }
        public string NomeSetor { get; set; }

        public int QtdMoto { get; set; }
        public int Capacidade { get; set; }

        public string Descricao { get; set; }
        public string Cor { get; set; }

        public string Localizacao { get; set; }
        public CreatePatioResponse Patio { get; set; }
    }
}
