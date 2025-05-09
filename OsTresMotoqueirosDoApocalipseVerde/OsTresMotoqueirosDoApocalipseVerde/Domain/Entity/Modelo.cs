using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Modelo
    {
        public Guid Id_Modelo { get; private set; }

        public string NomeModelo { get; private set; }

        public string Freagem { get; private set; }

        public string SistemaPartida { get; set; }

        public long Tanque { get; set; }

        public TipoCombustivel TipoCombustivel { get; set; }

        public long Consumo { get; set; }

        public Modelo(string nomeModelo, string freagem, string sistemaPartida, long tanque, TipoCombustivel tipoCombustivel, long consumo)
        {
           NomeModelo = nomeModelo ?? throw new DomainException($"Nome é obrigatorio");
           Freagem = freagem;
           SistemaPartida = sistemaPartida;
           Tanque = tanque;
           TipoCombustivel = tipoCombustivel;
           Consumo = consumo;
        }

        private void ValidadorTanque(long tanque)
        {

            if (tanque < 10)
                throw new DomainException($"Tanque incorreto: {tanque}. Verifique antes de criar.");

        }

        private void ValidadorConsumo(long consumo)
        {

            if (consumo < 10)
                throw new DomainException($"Consumo incorreto: {consumo}. Verifique antes de criar.");

        }

        internal static Modelo Create(string nomeModelo, string freagem, string sistemaPartida, long tanque, TipoCombustivel tipoCombustivel, long consumo)
        {
            return new Modelo(nomeModelo, freagem, sistemaPartida, tanque, tipoCombustivel, consumo);
        }

        public Modelo()
        {

        }
    }
}
