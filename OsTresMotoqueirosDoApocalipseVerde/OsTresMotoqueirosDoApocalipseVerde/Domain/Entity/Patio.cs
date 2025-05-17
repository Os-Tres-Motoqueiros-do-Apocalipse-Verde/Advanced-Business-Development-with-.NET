using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Patio
    {
        public long IdPatio { get; private set; }

        public int TotalMotos { get; private set; }

        public int CapacidadeMoto { get; set; }

        public int AreaPatio { get; set; }

        // Relacionamento 
        public long FilialId { get; private set; }
        public virtual Filial Filial { get; private set; }

        private readonly List<Setor> _setores = new();
        public virtual IReadOnlyCollection<Setor> Setores => _setores.AsReadOnly();


        public Patio( int totalMotos, int capacidadeMoto, int areaPatio, long filialId)
        {
            TotalMotos = totalMotos;
            CapacidadeMoto = capacidadeMoto;
            AreaPatio = areaPatio;
            FilialId = filialId;
        }

        public Setor AddSetor(int quantidadeMoto, int capacidade, long areaSetor, string nomeSetor, string descricao, long patioId)
        {
            var setor = Setor.Create(quantidadeMoto, capacidade, areaSetor, nomeSetor, descricao, patioId);
            _setores.Add(setor);

            return setor;
        }

        private void VerificadorCapacidade(int capacidadeMoto)
        {
            if (capacidadeMoto > 10)
                throw new DomainException($"A capacidade do patio esta errada {capacidadeMoto}, verifique antes de criar");
        }

        private void VerificadorArea(int areaPatio)
        {
            if (areaPatio > 10)
                throw new DomainException($"A area do patio esta errada: {areaPatio}, verifique antes de criar");
        }

        internal static Patio Create(int totalMotos, int capacidadeMoto, int areaPatio, long filialId)
        {
            return new Patio(totalMotos, capacidadeMoto, areaPatio, filialId);
        }


        public Patio()
        {
            _setores = new List<Setor>();
        }
    }
}
