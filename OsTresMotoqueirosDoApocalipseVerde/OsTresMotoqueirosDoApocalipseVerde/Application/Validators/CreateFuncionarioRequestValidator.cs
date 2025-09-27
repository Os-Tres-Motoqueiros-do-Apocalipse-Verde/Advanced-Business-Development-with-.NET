using FluentValidation;
using OsTresMotoqueirosDoApocalipseVerde.Application.DTOs.Request;

namespace OsTresMotoqueirosDoApocalipseVerde.Application.Validators
{
    public class CreateFuncionarioRequestValidator : AbstractValidator<CreateFuncionarioRequest>
    {
        public CreateFuncionarioRequestValidator()
        {
        }
    }
}
