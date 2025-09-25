namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Response
{
    public class CreatePatioResponse
    {
        public long Id { get; set; }
        public int TotalMotos { get; set; }
        public int CapacidadeMoto { get; set; }

        //chave estrangeira
        public string Localizacao { get; set; }
        public long FilialId { get; set; }
    }
}
