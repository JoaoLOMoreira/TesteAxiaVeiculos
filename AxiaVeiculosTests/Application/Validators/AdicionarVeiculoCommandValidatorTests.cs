using AxiaVeiculosApplication.Veiculos.Commands;
using AxiaVeiculosApplication.Veiculos.Validators;
using AxiaVeiculosDomain.Enumerators;
using FluentAssertions;

namespace AxiaVeiculosTests.Application.Validators;

public sealed class AdicionarVeiculoCommandValidatorTests
{
    private readonly AdicionarVeiculoCommandValidator _validator = new();

    [Fact]
    public async Task ValidateAsync_QuandoDadosValidos_DeveSerValido()
    {
        var command = new AdicionarVeiculoCommand(
            "Honda Civic EXL",
            MarcaVeiculo.Honda,
            "Civic",
            "Cambio automatico",
            145000m);

        var result = await _validator.ValidateAsync(command);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task ValidateAsync_QuandoCamposObrigatoriosInvalidos_DeveRetornarMensagens()
    {
        var command = new AdicionarVeiculoCommand(
            string.Empty,
            null,
            string.Empty,
            null,
            -1m);

        var result = await _validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors
            .Select(error => error.ErrorMessage)
            .Should()
            .Contain(new[]
            {
                "A descricao e obrigatoria.",
                "A marca e obrigatoria.",
                "O modelo e obrigatorio.",
                "O valor nao pode ser negativo."
            });
    }

    [Theory]
    [InlineData(MarcaVeiculo.NaoInformada)]
    [InlineData((MarcaVeiculo)999)]
    public async Task ValidateAsync_QuandoMarcaInvalida_DeveRetornarMensagem(
        MarcaVeiculo marca)
    {
        var command = new AdicionarVeiculoCommand(
            "Honda Civic EXL",
            marca,
            "Civic",
            null,
            145000m);

        var result = await _validator.ValidateAsync(command);

        result.IsValid.Should().BeFalse();
        result.Errors
            .Select(error => error.ErrorMessage)
            .Should()
            .Contain("A marca informada nao e valida.");
    }
}
