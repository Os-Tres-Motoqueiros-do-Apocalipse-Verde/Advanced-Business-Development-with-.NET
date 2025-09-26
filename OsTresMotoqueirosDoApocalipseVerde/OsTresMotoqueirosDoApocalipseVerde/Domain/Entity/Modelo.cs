using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Modelo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string NomeModelo { get; set; }

        public Frenagem Frenagem { get; set; }

        public SistemaPartida SistemaPartida { get; set; }
        public int Tanque { get; set; }
        public TipoCombustivel TipoCombustivel { get; set; }
        public int Consumo { get; set; }

        public virtual Moto Moto { get; set; }

        private Modelo(string nomeModelo, Frenagem frenagem, SistemaPartida sistemaPartida, int tanque, TipoCombustivel tipoCombustivel, int consumo)
        {
            NomeModelo = nomeModelo;
            Frenagem = frenagem;
            SistemaPartida = sistemaPartida;
            Tanque = tanque;
            TipoCombustivel = tipoCombustivel;
            Consumo = consumo;
        }

        public void Atualizar(string nomeModelo, Frenagem frenagem, SistemaPartida sistemaPartida, int tanque, TipoCombustivel tipoCombustivel, int consumo)
        {
            NomeModelo = nomeModelo;
            Frenagem = frenagem;
            SistemaPartida = sistemaPartida;
            Tanque = tanque;
            TipoCombustivel = tipoCombustivel;
            Consumo = consumo;
        }


        internal static Modelo Create(string nomeModelo, Frenagem frenagem, SistemaPartida sistemaPartida, int tanque, TipoCombustivel tipoCombustivel, int consumo)
        {
            return new Modelo(nomeModelo, frenagem, sistemaPartida, tanque, tipoCombustivel, consumo);
        }

        public Modelo() { }

    }
}
