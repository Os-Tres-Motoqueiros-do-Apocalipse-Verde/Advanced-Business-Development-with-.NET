namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreatePatioRequest
    {
        public int TotalMotos { get; set; }
        public int CapacidadeMoto {  get; set; }

        //chave estrangeira
        public CreateRegiaoRequest Regiao { get; set; }

        public long? FilialId { get; set; }
    }
}
