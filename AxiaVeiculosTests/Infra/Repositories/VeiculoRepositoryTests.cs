using AxiaVeiculosDomain.Entities;
using AxiaVeiculosDomain.Enumerators;
using AxiaVeiculosInfra.Data;
using AxiaVeiculosInfra.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AxiaVeiculosTests.Infra.Repositories;

public sealed class VeiculoRepositoryTests
{
    [Fact]
    public async Task AdicionarEListarAsync_DevePersistirVeiculoComInMemory()
    {
        await using var context = CriarContexto();
        var repository = new VeiculoRepository(context);
        var veiculo = new Veiculo(
            "Toyota Corolla",
            MarcaVeiculo.Toyota,
            "Corolla",
            null,
            120000m);

        await repository.AdicionarAsync(veiculo, CancellationToken.None);
        await repository.SalvarAlteracoesAsync(CancellationToken.None);

        var veiculos = await repository.ListarAsync(CancellationToken.None);

        veiculos.Should().ContainSingle(item => item.Id == veiculo.Id);
    }

    [Fact]
    public async Task ObterPorIdSomenteLeituraAsync_DeveRetornarEntidadeSemTracking()
    {
        await using var context = CriarContexto();
        var repository = new VeiculoRepository(context);
        var veiculo = new Veiculo(
            "Honda Civic",
            MarcaVeiculo.Honda,
            "Civic",
            null,
            145000m);

        await repository.AdicionarAsync(veiculo, CancellationToken.None);
        await repository.SalvarAlteracoesAsync(CancellationToken.None);
        context.ChangeTracker.Clear();

        var veiculoSomenteLeitura = await repository.ObterPorIdSomenteLeituraAsync(
            veiculo.Id,
            CancellationToken.None);

        veiculoSomenteLeitura.Should().NotBeNull();
        context.Entry(veiculoSomenteLeitura!).State.Should().Be(EntityState.Detached);
    }

    [Fact]
    public async Task AtualizarEExcluirAsync_DevePersistirAlteracoes()
    {
        await using var context = CriarContexto();
        var repository = new VeiculoRepository(context);
        var veiculo = new Veiculo(
            "Fiat Pulse",
            MarcaVeiculo.Fiat,
            "Pulse",
            null,
            98000m);

        await repository.AdicionarAsync(veiculo, CancellationToken.None);
        await repository.SalvarAlteracoesAsync(CancellationToken.None);

        var veiculoParaAtualizar = await repository.ObterPorIdAsync(
            veiculo.Id,
            CancellationToken.None);
        veiculoParaAtualizar!.Atualizar(
            "Fiat Pulse Impetus",
            MarcaVeiculo.Fiat,
            "Pulse",
            "Teto solar",
            112000m);

        repository.Atualizar(veiculoParaAtualizar);
        await repository.SalvarAlteracoesAsync(CancellationToken.None);
        context.ChangeTracker.Clear();

        var veiculoAtualizado = await repository.ObterPorIdSomenteLeituraAsync(
            veiculo.Id,
            CancellationToken.None);

        veiculoAtualizado!.Descricao.Should().Be("Fiat Pulse Impetus");
        veiculoAtualizado.Opcionais.Should().Be("Teto solar");

        var veiculoParaExcluir = await repository.ObterPorIdAsync(
            veiculo.Id,
            CancellationToken.None);
        repository.Excluir(veiculoParaExcluir!);
        await repository.SalvarAlteracoesAsync(CancellationToken.None);

        var veiculoExcluido = await repository.ObterPorIdSomenteLeituraAsync(
            veiculo.Id,
            CancellationToken.None);

        veiculoExcluido.Should().BeNull();
    }

    private static VeiculosDbContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<VeiculosDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new VeiculosDbContext(options);
    }
}
