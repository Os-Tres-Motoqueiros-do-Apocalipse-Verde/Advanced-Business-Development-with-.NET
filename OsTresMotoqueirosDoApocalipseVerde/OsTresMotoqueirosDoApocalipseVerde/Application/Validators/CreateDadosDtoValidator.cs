using FluentValidation;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;

public class CreateDadosDtoValidator : AbstractValidator<CreateDadosRequest>
{
    public CreateDadosDtoValidator()
    {
        RuleFor(x => x.CPF)
            .NotEmpty().WithMessage("CPF é obrigatório")
            .Length(11).WithMessage("CPF deve ter exatamente 11 caracteres");

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("Telefone é obrigatório")
            .MaximumLength(11).WithMessage("Telefone deve ter no máximo 13 caracteres");


        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(150).WithMessage("Nome deve ter no máximo 150 caracteres");
    }
}
