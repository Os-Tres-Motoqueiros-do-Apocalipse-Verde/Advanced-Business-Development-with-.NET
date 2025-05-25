using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;

public class ReadModeloDto
{
    public int Id { get; set; }
    public string NomeModelo { get; set; }
    public Frenagem Frenagem { get; set; }
    public SistemaPartida SistemaPartida { get; set; }
    public int Tanque { get; set; }
    public TipoCombustivel TipoCombustivel { get; set; }
    public int Consumo { get; set; }
}
