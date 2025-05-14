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

        public float Longitude { get; set; }

        //Relacionamento 1..1
        public Guid ModeloId { get; private set; }
        public virtual Modelo Modelo { get; private set; }

        public Guid SetorId { get; private set; }
        public virtual Setor IdSetor { get; set; }

        public Guid MotoristaId { get; private set; }
        public virtual Motorista IdMotorista { get; set; }

        //Relacionamento N..N
        private readonly List<Situacao> _situacoes = new();
        public virtual IReadOnlyCollection<Situacao> Situacoes => _situacoes.AsReadOnly();

        public Moto(string placa, string chassi, string condicao, float latitude, float longitude, Guid modeloId, Guid setorId, Guid motoristaId)
        {
            IdMoto = Guid.NewGuid();
            Placa = placa;
            Chassi = chassi;
            Condicao = condicao;
            Latitude = latitude;
            Longitude = longitude;
            ModeloId = modeloId;
            SetorId = setorId;
            MotoristaId = motoristaId;
        }

        public Situacao AddSituacao(string nome, string descricao, Status status)
        {
            var situacao = Situacao.Create(nome, descricao, status);
            _situacoes.Add(situacao);

            return situacao;
        }

        internal static Moto Create(string placa, string chassi, string condicao, float latitude, float longitude, Guid modeloId, Guid setorId, Guid motoristaId)
        {
            return new Moto(placa, chassi, condicao, latitude, longitude, modeloId, setorId, motoristaId);
        }


        public Moto()
        {
            _situacoes = new List<Situacao>();
        }

    }
}
