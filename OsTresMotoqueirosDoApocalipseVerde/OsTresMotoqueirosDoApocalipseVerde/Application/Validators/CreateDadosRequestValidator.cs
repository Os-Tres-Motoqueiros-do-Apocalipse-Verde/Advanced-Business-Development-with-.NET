using FluentValidation;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;
using System.Linq;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.Validators
{
    public class CreateDadosRequestValidator : AbstractValidator<CreateDadosRequest>
    {
        public CreateDadosRequestValidator()
        {
            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("CPF é obrigatório")
                .Must(cpf =>
                {
                    if (string.IsNullOrWhiteSpace(cpf)) return false;

                    // Remove qualquer coisa que não seja número
                    var digitsOnly = new string(cpf.Where(char.IsDigit).ToArray());

                    // Verifica se tem exatamente 11 dígitos
                    return digitsOnly.Length == 11;
                })
                .WithMessage("CPF deve conter exatamente 11 dígitos numéricos.");
        }
    }
}
