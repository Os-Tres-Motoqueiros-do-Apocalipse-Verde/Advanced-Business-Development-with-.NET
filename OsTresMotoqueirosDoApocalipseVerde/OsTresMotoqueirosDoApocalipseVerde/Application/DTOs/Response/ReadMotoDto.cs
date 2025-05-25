namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class ReadMotoDto
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Chassi { get; set; }
        public string Condicao { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public ReadModeloDto Modelo { get; set; }
    }
}
