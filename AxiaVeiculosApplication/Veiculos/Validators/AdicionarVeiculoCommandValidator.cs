using AxiaVeiculosApplication.Veiculos.Commands;
using AxiaVeiculosDomain.Enumerators;
using FluentValidation;

namespace AxiaVeiculosApplication.Veiculos.Validators;

public sealed class AdicionarVeiculoCommandValidator : AbstractValidator<AdicionarVeiculoCommand>
{
    public AdicionarVeiculoCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(command => command.Descricao)
            .NotEmpty()
            .WithMessage("A descricao e obrigatoria.")
            .MaximumLength(200)
            .WithMessage("A descricao deve ter no maximo 200 caracteres.");

        RuleFor(command => command.Modelo)
            .NotEmpty()
            .WithMessage("O modelo e obrigatorio.")
            .MaximumLength(100)
            .WithMessage("O modelo deve ter no maximo 100 caracteres.");

        RuleFor(command => command.Marca)
            .NotNull()
            .WithMessage("A marca e obrigatoria.");

        RuleFor(command => command.Marca)
            .Must(marca => marca.HasValue
                && Enum.IsDefined(typeof(MarcaVeiculo), marca.Value)
                && marca.Value != MarcaVeiculo.NaoInformada)
            .When(command => command.Marca.HasValue)
            .WithMessage("A marca informada nao e valida.");

        RuleFor(command => command.Opcionais)
            .MaximumLength(500)
            .WithMessage("Os opcionais devem ter no maximo 500 caracteres.");

        RuleFor(command => command.Valor)
            .GreaterThanOrEqualTo(0)
            .When(command => command.Valor.HasValue)
            .WithMessage("O valor nao pode ser negativo.");
    }
}
