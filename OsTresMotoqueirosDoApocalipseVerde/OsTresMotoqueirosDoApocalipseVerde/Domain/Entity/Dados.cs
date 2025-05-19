using OsTresMotoqueirosDoApocalipseVerde.Domain.Entity;
using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.Text.RegularExpressions;

public class Dados
{

    public int Id { get; private set; }
    public string CPF { get;  set; }
    public string Telefone { get;  set; }
    public string Email { get;  set; }
    public string Senha { get;  set; }
    public string Nome { get;  set; }


    public Dados(string cpf, string telefone, string email, string senha, string nome)
    {
       
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
    public static Dados Create(string cpf, string telefone, string email, string senha, string nome)
    {
        return new Dados(cpf, telefone, email, senha, nome );
    }

    public Dados()
    {

    }
}