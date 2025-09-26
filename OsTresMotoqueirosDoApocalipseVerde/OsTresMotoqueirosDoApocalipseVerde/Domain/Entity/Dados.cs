using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Dados
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        public virtual Motorista Motorista { get; set; }

        private Dados(string cpf, string telefone, string email, string senha, string nome)
        {
            CPF = cpf;
            Telefone = telefone;
            Email = email;
            Senha = senha;
            Nome = nome;
        }

        public void Atualizar(string cpf, string telefone, string email, string senha, string nome)
        {
            CPF = cpf;
            Telefone = telefone;
            Email = email;
            Senha = senha;
            Nome = nome;
        }


        internal static Dados Create(string cpf, string telefone, string email, string senha, string nome)
        {
            return new Dados(cpf, telefone, email, senha, nome);
        }

        public Dados() { }
    }

}
