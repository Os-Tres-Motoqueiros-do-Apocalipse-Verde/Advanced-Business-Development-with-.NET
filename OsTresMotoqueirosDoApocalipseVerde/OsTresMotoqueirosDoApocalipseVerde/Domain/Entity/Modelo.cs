using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.IO;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Modelo
    {
        public Guid IdModelo { get; private set; }

        public string NomeModelo { get; private set; }

        public Frenagem Frenagem { get; private set; }

        public SistemaPartida SistemaPartida { get; set; }

        public float Tanque { get; set; }

        public TipoCombustivel TipoCombustivel { get; set; }

        public float Consumo { get; set; }

        //Relacionamento 1..N
        private readonly List<Moto> _motos = new();
        public virtual IReadOnlyCollection<Moto> Motos => _motos.AsReadOnly();

        public Moto AddMoto( string placa, string chassi, string condicao, float latitude, float longitude)
        {
            var moto = Moto.Create( placa, chassi, condicao, longitude, latitude);
            _motos.Add(moto);

            return moto;
        }


        public Modelo(string nomeModelo, Frenagem frenagem, SistemaPartida sistemaPartida, float tanque, TipoCombustivel tipoCombustivel, float consumo)
        {
           NomeModelo = nomeModelo ?? throw new DomainException($"Nome é obrigatorio");
           Frenagem = frenagem;
           SistemaPartida = sistemaPartida;
           Tanque = tanque;
           TipoCombustivel = tipoCombustivel;
           Consumo = consumo;
        }

        private void ValidadorTanque(float tanque)
        {

            if (tanque < 10)
                throw new DomainException($"Tanque incorreto: {tanque}. Verifique antes de criar.");

        }

        private void ValidadorConsumo(long consumo)
        {

            if (consumo < 10)
                throw new DomainException($"Consumo incorreto: {consumo}. Verifique antes de criar.");

        }

        internal static Modelo Create(Guid idModelo, string nomeModelo, Frenagem frenagem, SistemaPartida sistemaPartida, float tanque, TipoCombustivel tipoCombustivel, float consumo, Moto moto = null)
        {
            var modelo = new Modelo(idModelo, nomeModelo, frenagem, sistemaPartida, tanque, tipoCombustivel, consumo, moto);

            if (moto != null)
            {
                modelo.moto = moto;
                modelo.MotoId = moto.IdMoto;
            }
            return modelo;
        }

        public Modelo()
        {

        }
    }
}
