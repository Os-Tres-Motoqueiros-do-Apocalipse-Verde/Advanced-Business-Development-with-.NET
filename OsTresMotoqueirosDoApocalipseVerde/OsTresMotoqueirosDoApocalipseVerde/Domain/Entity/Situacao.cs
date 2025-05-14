using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Situacao
    {
        public Guid IdSituacao { get; private set; }

        public string Nome { get; private set; }

        public string Descricao { get; set; }

        public Status Status { get; set; }

        //Relacionamento N..N
        private readonly List<Moto> _motos = new();
        public virtual IReadOnlyCollection<Moto> Motos => _motos.AsReadOnly();

        public Situacao(string nome, string descricao, Status status)
        {
            IdSituacao = Guid.NewGuid();
            Nome = nome ?? throw new DomainException($"Nome é obrigatorio");
            Descricao = descricao;
            Status = status;
        }

        public Moto AddMoto(string placa, string chassi, string condicao, float latitude, float longitude, Guid modeloId, Guid setorId, Guid motoristaId)
        {
            var moto = Moto.Create(placa, chassi, condicao, latitude, longitude, modeloId, setorId, motoristaId);
            _motos.Add(moto);

            return moto;
        }

        internal static Situacao Create(string nome, string descricao, Status status)
        {
            return new Situacao(nome, descricao, status);
        }


        public Situacao()
        {
            _motos = new List<Moto>();
        }
    }
}
