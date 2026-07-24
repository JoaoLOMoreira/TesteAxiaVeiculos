using AxiaVeiculosApplication.Common.Exceptions;
using AxiaVeiculosApplication.Veiculos.Commands;
using AxiaVeiculosApplication.Veiculos.Services;
using AxiaVeiculosDomain.Entities;
using AxiaVeiculosDomain.Enumerators;
using AxiaVeiculosDomain.Repositories;
using FluentAssertions;
using Moq;

namespace AxiaVeiculosTests.Application.Services;

public sealed class VeiculoServiceTests
{
    [Fact]
    public async Task AdicionarAsync_DeveCriarVeiculoESalvarAlteracoes()
    {
        var repository = new Mock<IVeiculoRepository>();
        Veiculo? veiculoAdicionado = null;

        repository
            .Setup(repo => repo.AdicionarAsync(It.IsAny<Veiculo>(), It.IsAny<CancellationToken>()))
            .Callback<Veiculo, CancellationToken>((veiculo, _) => veiculoAdicionado = veiculo)
            .Returns(Task.CompletedTask);

        repository
            .Setup(repo => repo.SalvarAlteracoesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var service = new VeiculoService(repository.Object);

        var response = await service.AdicionarAsync(new AdicionarVeiculoCommand(
            "  Toyota Corolla  ",
            MarcaVeiculo.Toyota,
            "  Corolla  ",
            "  Bancos em couro  ",
            120000m), CancellationToken.None);

        response.Id.Should().NotBeEmpty();
        response.Descricao.Should().Be("Toyota Corolla");
        response.Modelo.Should().Be("Corolla");
        response.Opcionais.Should().Be("Bancos em couro");
        veiculoAdicionado.Should().NotBeNull();

        repository.Verify(repo => repo.AdicionarAsync(
            It.IsAny<Veiculo>(),
            It.IsAny<CancellationToken>()), Times.Once);
        repository.Verify(repo => repo.SalvarAlteracoesAsync(
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task AtualizarAsync_QuandoVeiculoNaoExiste_DeveLancarExcecao()
    {
        var repository = new Mock<IVeiculoRepository>();
        repository
            .Setup(repo => repo.ObterPorIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Veiculo?)null);

        var service = new VeiculoService(repository.Object);

        var act = () => service.AtualizarAsync(new AtualizarVeiculoCommand(
            Guid.NewGuid(),
            "Honda Civic",
            MarcaVeiculo.Honda,
            "Civic",
            null,
            145000m), CancellationToken.None);

        await act.Should().ThrowAsync<VeiculoNaoEncontradoException>();
    }

    [Fact]
    public async Task ExcluirAsync_QuandoVeiculoExiste_DeveRemoverESalvarAlteracoes()
    {
        var veiculo = new Veiculo(
            "Fiat Pulse",
            MarcaVeiculo.Fiat,
            "Pulse",
            null,
            98000m);

        var repository = new Mock<IVeiculoRepository>();
        repository
            .Setup(repo => repo.ObterPorIdAsync(veiculo.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(veiculo);
        repository
            .Setup(repo => repo.SalvarAlteracoesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var service = new VeiculoService(repository.Object);

        await service.ExcluirAsync(veiculo.Id, CancellationToken.None);

        repository.Verify(repo => repo.Excluir(veiculo), Times.Once);
        repository.Verify(repo => repo.SalvarAlteracoesAsync(
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
