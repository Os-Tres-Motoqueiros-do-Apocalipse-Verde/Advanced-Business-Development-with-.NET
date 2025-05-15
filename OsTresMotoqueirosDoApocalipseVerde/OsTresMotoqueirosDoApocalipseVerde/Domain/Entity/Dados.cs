using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Text.RegularExpressions;

public class Dados
{
    public Guid Id { get; private set; }
    public string CPF { get; private set; }
    public string Telefone { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }
    public string Nome { get; private set; }

    // Relacionamentos
    public Guid? FuncionarioId { get; private set; }
    public virtual Funcionario Funcionario { get; private set; }

    public Guid? MotoristaId { get; private set; }
    public virtual Motorista Motorista { get; private set; }

    private Dados(string cpf, string telefone, string email, string senha, string nome)
    {
        Id = Guid.NewGuid();
        CPF = cpf;
        Telefone = telefone;
        Email = email;
        Senha = senha;
        Nome = nome;
    }

    private void VerificadorSenha(string senha)
    {

        if (senha.Length < 8)
            throw new DomainException($"Senha incorreta: {senha}. Verifique antes de criar.");

        var regex = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$", RegexOptions.IgnoreCase);

        if (!regex.IsMatch(senha))
            throw new DomainException($"Senha inválida: {senha}");
    }
    public static Dados Create(string cpf, string telefone, string email, string senha, string nome, Funcionario funcionario = null, Motorista motorista = null)
    {
        if (funcionario != null && motorista != null)
            throw new DomainException("Dados não podem estar vinculados a um funcionário e a um motorista ao mesmo tempo.");

        if (funcionario == null && motorista == null)
            throw new DomainException("Dados devem estar vinculados a um funcionário ou a um motorista.");

        var dados = new Dados(cpf, telefone, email, senha, nome);

        if (funcionario != null)
        {
            dados.Funcionario = funcionario;
            dados.FuncionarioId = funcionario.IdFuncionario;
        }

        if (motorista != null)
        {
            dados.Motorista = motorista;
            dados.MotoristaId = motorista.IdMotorista;
        }
        return dados;
    }

    public Dados()
    {

    }
}