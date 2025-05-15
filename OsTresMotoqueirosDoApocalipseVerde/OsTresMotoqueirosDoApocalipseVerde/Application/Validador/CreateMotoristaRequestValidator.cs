using FluentValidation;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.Validador
{
    public class CreateMotoristaRequestValidator : AbstractValidator<CreatedMotoristaRequest>
    {
        public CreateMotoristaRequestValidator()
        {
            RuleFor(m => m.Plano)
                .NotEmpty()
                .WithMessage("O plano é obrigatório.")
                .MaximumLength(20)
                .WithMessage("O plano deve ter no máximo 20 caracteres.");

            RuleFor(m => m.DadosId)
                .NotEmpty()
                .WithMessage("O ID dos dados é obrigatório.");
        }
    }
}