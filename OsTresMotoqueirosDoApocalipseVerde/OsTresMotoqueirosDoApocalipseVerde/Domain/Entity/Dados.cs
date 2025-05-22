using OsTresMotoqueirosDoApocalipseVerde.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class Dados
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string CPF { get;  set; }

    public string Telefone { get;  set; }
    public string Email { get;  set; }
    public string Senha { get;  set; }

    [Required]
    public string Nome { get;  set; }

}