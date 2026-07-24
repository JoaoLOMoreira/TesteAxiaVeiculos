using AxiaVeiculosApplication.Veiculos.Queries;
using FluentValidation;

namespace AxiaVeiculosApplication.Veiculos.Validators;

public sealed class ObterVeiculoPorIdQueryValidator : AbstractValidator<ObterVeiculoPorIdQuery>
{
    public ObterVeiculoPorIdQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEmpty()
            .WithMessage("O id e obrigatorio.");
    }
}
