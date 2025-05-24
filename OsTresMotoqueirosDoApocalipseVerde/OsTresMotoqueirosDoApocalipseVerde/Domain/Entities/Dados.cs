using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Dados
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string CPF { get; set; }

    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }

    [Required]
    public string Nome { get; set; }
}
