using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Moto
    {
        public int IdMoto { get; private set; }

        public string Placa { get; private set; }

        public string Chassi { get; private set; }

        public string Condicao { get; set; }

        public float Latitude { get; set; }

        public float intitude { get; set; }

        //Relacionamento 1..1
        public int ModeloId { get; private set; }
        public virtual Modelo Modelo { get; private set; }

        public int SetorId { get; private set; }
        public virtual Setor Setor { get; set; }

        public int MotoristaId { get; private set; }
        public virtual Motorista Motorista { get; set; }

        //Relacionamento N..N
        private readonly List<Situacao> _situacoes = new();
        public virtual IReadOnlyCollection<Situacao> Situacoes => _situacoes.AsReadOnly();

        public Moto(string placa, string chassi, string condicao, float latitude, float intitude, int modeloId, int setorId, int motoristaId)
        {
            Placa = placa;
            Chassi = chassi;
            Condicao = condicao;
            Latitude = latitude;
            intitude = intitude;
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

        internal static Moto Create(string placa, string chassi, string condicao, float latitude, float intitude, int modeloId, int setorId, int motoristaId)
        {
            return new Moto(placa, chassi, condicao, latitude, intitude, modeloId, setorId, motoristaId);
        }


        public Moto()
        {
            _situacoes = new List<Situacao>();
        }

    }
}
