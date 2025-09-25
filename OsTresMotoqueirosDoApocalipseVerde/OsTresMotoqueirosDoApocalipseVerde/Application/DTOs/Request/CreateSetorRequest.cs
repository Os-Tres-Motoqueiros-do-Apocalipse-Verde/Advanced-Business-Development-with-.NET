namespace OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request
{
    public class CreateSetorRequest
    {
        public string NomeSetor { get; set;}

        public int QtdMoto {  get; set; }
        public int Capacidade { get; set; }

        public string Descricao { get; set; }
        public string Cor {  get; set; }

        public string Localizacao { get; set; }

        public long PatioId { get; set; }

    }
}
