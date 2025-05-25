namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class UpdateMotoDto
    {
        public string Placa { get; set; }
        public string Chassi { get; set; }
        public string Condicao { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        // Foreign key
        public int? ModeloId { get; set; }
    }
}
