using FluentValidation;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.Validador
{
    public class CreateMotoristaDtoValidator : AbstractValidator<CreateMotoristaDto>
    {
        public CreateMotoristaDtoValidator()
        {
            RuleFor(m => m.Plano)
                .IsInEnum()
                .WithMessage("O plano selecionado é inválido.");

            RuleFor(m => m.DadosId)
                .NotEmpty()
                .WithMessage("O ID dos dados é obrigatório.");
        }
    }
}