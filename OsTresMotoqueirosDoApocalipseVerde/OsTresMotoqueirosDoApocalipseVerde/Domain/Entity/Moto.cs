using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Moto
    {
        public Guid IdMoto { get; private set; }

        public string Placa { get; private set; }

        public string Chassi { get; private set; }

        public string Condicao { get; set; }

        public float Latitude { get; set; }

        public float Longitute { get; set; }

        //Relacionamento 1..1
        public Guid ModeloId { get; private set; }
        public virtual Modelo Modelo { get; private set; }

        public Guid SetorId { get; private set; }
        public virtual Setor IdSetor { get; set; }

        public Guid MotoristaId { get; private set; }
        public virtual Motorista IdMotorista { get; set; }


        public Situacao IdSituacao { get; set; }

        public Moto(string placa, string chassi, string condicao, float latitude, float longitude, Guid idModelo, Guid idSetor, Guid idMotorista, Situacao idSituacao)
        {
            IdMoto = Guid.NewGuid();
            Placa = placa;
            Chassi = chassi;
            Condicao = condicao ?? throw new DomainException($"condição é obrigatorio");
            Latitude = latitude;
            Longitute = longitude;
            ModeloId = idModelo;
            SetorId = idSetor;
            MotoristaId = idMotorista;
        }

        internal static Moto Create(string placa, string chassi, string condicao, float latitude, float longitude, Guid modeloId)
        {
            return new Moto(placa, chassi, condicao, latitude, longitude);
        }


        public Moto()
        {

        }
    }
}
