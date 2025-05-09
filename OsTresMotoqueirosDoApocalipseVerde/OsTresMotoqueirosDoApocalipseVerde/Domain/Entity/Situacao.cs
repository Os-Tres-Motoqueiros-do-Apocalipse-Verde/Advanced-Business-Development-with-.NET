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

        public Situacao(string nome, string descricao, Status status)
        {
            IdSituacao = Guid.NewGuid();
            Nome = nome ?? throw new DomainException($"Nome é obrigatorio");
            Descricao = descricao;
            Status = status;
        }

        internal static Situacao Create(string nome, string descricao, Status status)
        {
            return new Situacao(nome, descricao, status);
        }


        public Situacao()
        {

        }
    }
}
