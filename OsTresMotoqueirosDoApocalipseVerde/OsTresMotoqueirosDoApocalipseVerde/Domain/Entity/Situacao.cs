using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Situacao
    {
        [Key]
        public int IdSituacao { get; private set; }

        public string Nome { get; private set; }

        public string Descricao { get; set; }

        public Status Status { get; set; }

        //Relacionamento N..N
        private readonly List<Moto> _motos = new();
        public virtual IReadOnlyCollection<Moto> Motos => _motos.AsReadOnly();

        public Situacao(string nome, string descricao, Status status)
        {
            Nome = nome ?? throw new DomainException($"Nome é obrigatorio");
            Descricao = descricao;
            Status = status;
        }

        public Moto AddMoto(string placa, string chassi, string condicao, float latitude, float intitude, int modeloId, int setorId, int motoristaId)
        {
            var moto = Moto.Create(placa, chassi, condicao, latitude, intitude, modeloId, setorId, motoristaId);
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
