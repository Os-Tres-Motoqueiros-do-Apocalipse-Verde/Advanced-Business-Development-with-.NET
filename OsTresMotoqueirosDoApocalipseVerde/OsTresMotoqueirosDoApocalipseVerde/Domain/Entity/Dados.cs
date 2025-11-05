using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OsTresMotoqueirosDoApocalipseVerde.Domain.Entity
{
    public class Dados
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public virtual Funcionario Funcionario { get; set; }
        public virtual Motorista Motorista { get; set; }

        private Dados(string nome, string cpf, string telefone, string email, string senha)
        {
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Email = email;
            Senha = senha;
            
        }

        public void Atualizar(string nome, string cpf, string telefone, string email, string senha)
        {
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Email = email;
            Senha = senha;
        }


        internal static Dados Create(string nome, string cpf, string telefone, string email, string senha)
        {
            return new Dados(nome, cpf, telefone, email, senha);
        }

        public Dados() { }
    }

}
