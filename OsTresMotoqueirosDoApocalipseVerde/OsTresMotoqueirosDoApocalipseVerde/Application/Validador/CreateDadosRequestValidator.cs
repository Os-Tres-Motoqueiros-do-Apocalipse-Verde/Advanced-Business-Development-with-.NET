using FluentValidation;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.Validador
{
    public class CreateDadosRequestValidator : AbstractValidator<CreateDadosRequest>
    {
        public CreateDadosRequestValidator()
        {
            RuleFor(d => d.CPF)
                .NotEmpty()
                .WithMessage("O CPF selecionado é inválido.");

            RuleFor(d => d.Telefone)
                .NotEmpty()
                .WithMessage("O Telefone selecionado é inválido.");

            RuleFor(d => d.Email)
                .NotEmpty()
                .WithMessage("O Email selecionado é inválido.");

            RuleFor(d => d.Senha)
                .NotEmpty()
                .WithMessage("A Senha selecionado é inválido.");

            RuleFor(d => d.Nome)
                .NotEmpty()
                .WithMessage("O Nome selecionado é inválido.");
        }
    }
}
