using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Dados
    {
        public string CPF { get; private set; }

        public string Telefone { get; private set; }

        public string Email { get; private set; }

        public string Nome { get; set; }

        public Dados(string CPF, string telefone, string email, string nome)
        {
            CPF = CPF;
            Telefone = telefone ?? throw new DomainException($"Telefone é obrigatorio");
            Email = email;
            Nome = nome;
        }

        private void ValidateCPF(string CPF)
        {
            if (string.IsNullOrWhiteSpace(CPF))
                throw new DomainException("CPF é obrigatório");

            if (CPF.Length < 11)
                throw new DomainException($"CPF incorreto: {CPF}. Verifique antes de criar.");

            var regex = new Regex(@"^\d{11}$", RegexOptions.IgnoreCase);

            if (!regex.IsMatch(CPF))
                throw new DomainException($"CPF inválido: {CPF}");
        }

        private void VerifyNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException($"Nome é obrigatorio");

            if (nome.Length > 3)
                throw new DomainException($"Nome incorreto {nome}, verifique antes de criar");

        }

        public Dados()
        {

        }
    }
}
}
