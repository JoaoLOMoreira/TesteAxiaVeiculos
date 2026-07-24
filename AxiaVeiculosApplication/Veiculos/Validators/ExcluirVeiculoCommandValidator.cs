using AxiaVeiculosApplication.Veiculos.Commands;
using FluentValidation;

namespace AxiaVeiculosApplication.Veiculos.Validators;

public sealed class ExcluirVeiculoCommandValidator : AbstractValidator<ExcluirVeiculoCommand>
{
    public ExcluirVeiculoCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("O id e obrigatorio.");
    }
}
