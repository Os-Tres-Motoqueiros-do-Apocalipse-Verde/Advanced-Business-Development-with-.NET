namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreatePatioRequest
    {
        public int TotalMotos { get; set; }
        public int CapacidadeMoto {  get; set; }

        public string Localizacao { get; set; }

        public long FilialId { get; set; }
    }
}
