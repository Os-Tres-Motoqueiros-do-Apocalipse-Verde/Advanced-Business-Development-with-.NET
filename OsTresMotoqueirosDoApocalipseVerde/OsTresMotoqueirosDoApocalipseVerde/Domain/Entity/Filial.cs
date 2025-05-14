using OsTresMotoqueirosDoApocalipseVerde.Domain.Enum;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Filial
    {
        public Guid IdFilial { get; private set; }

        public string Nome { get; private set; }

        public int TotalMotos { get; private set; }

        public int CapacidadeMoto { get; set; }

        public int AreaPatio { get; set; }

        public Funcionario IdResponsavel { get; set; }

        //Relacionamento 1..N
        private readonly List<Funcionario> _funcionarios = new();
        public virtual IReadOnlyCollection<Funcionario> Funcionarios => _funcionarios.AsReadOnly();

        public Setor IdSetor { get; set; }

        public Endereco Endereco { get; set; }

        //Relacionamento 1..N
        private readonly List<Setor> _setores = new();
        public virtual IReadOnlyCollection<Setor> Setores => _setores.AsReadOnly();

        public Filial(string nome, int totalMotos, int capacidadeMoto, int areaPatio, Funcionario idResposavel, Setor idSetor, Endereco endereco)
        {
            IdFilial = Guid.NewGuid();
            Nome = nome;
            TotalMotos = totalMotos;
            CapacidadeMoto = capacidadeMoto;
            AreaPatio = areaPatio;
            IdResponsavel = idResposavel;
            IdSetor = idSetor;
            Endereco = endereco;
        }

        public Setor AddSetor(int quantidadeMoto, int capacidade, long areaSetor, string nomeSetor, string descricao)
        {
            var setor = Setor.Create(quantidadeMoto, capacidade, areaSetor, nomeSetor, descricao);
            _setores.Add(setor);

            return setor;
        }

        public Funcionario AddFuncionario(Cargo cargo, Guid filialId, Dados dadosCpf){ 
            var funcionario = Funcionario.Create(cargo, filialId, dadosCpf);
            _funcionarios.Add(funcionario);

            return funcionario;
        }

        public void AtribuirEndereco(int numero, string estado, string codigoPais, string codigoPostal, string complemento, string rua)
        {
            if (Endereco != null)
                throw new InvalidOperationException("Esta Filial já possui endereco.");

            Endereco = Endereco.Create(numero, estado, codigoPais, codigoPostal, complemento, rua, filial: this);
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

        internal static Filial Create(string nome, int totalMotos, int capacidadeMoto, int areaPatio, Funcionario idResponsavel, Setor idSetor, Endereco endereco)
        {
            if (idResponsavel.Cargo != Cargo.Gerente)
                throw new DomainException("O responsável pela filial deve ter o cargo de Gerente.");

            return new Filial(nome, totalMotos, capacidadeMoto, areaPatio, idResponsavel, idSetor, endereco);
        }


        public Filial()
        {
            _setores = new List<Setor>();
            _funcionarios = new List<Funcionario>();
        }
    }
}
