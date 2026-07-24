using AxiaVeiculosApplication.Common.Exceptions;
using AxiaVeiculosApplication.Veiculos.Commands;
using AxiaVeiculosApplication.Veiculos.Responses;
using AxiaVeiculosDomain.Entities;
using AxiaVeiculosDomain.Repositories;

namespace AxiaVeiculosApplication.Veiculos.Services;

public sealed class VeiculoService : IVeiculoService
{
    private readonly IVeiculoRepository _veiculoRepository;

    public VeiculoService(IVeiculoRepository veiculoRepository)
    {
        _veiculoRepository = veiculoRepository;
    }

    public async Task<VeiculoResponse> AdicionarAsync(AdicionarVeiculoCommand command, CancellationToken cancellationToken)
    {
        var veiculo = new Veiculo(
            command.Descricao!,
            command.Marca!.Value,
            command.Modelo!,
            command.Opcionais,
            command.Valor);

        await _veiculoRepository.AdicionarAsync(veiculo, cancellationToken);
        await _veiculoRepository.SalvarAlteracoesAsync(cancellationToken);

        return MapearParaResponse(veiculo);
    }

    public async Task AtualizarAsync(AtualizarVeiculoCommand command, CancellationToken cancellationToken)
    {
        var veiculo = await _veiculoRepository.ObterPorIdAsync(command.Id, cancellationToken)
            ?? throw new VeiculoNaoEncontradoException();

        veiculo.Atualizar(
            command.Descricao!,
            command.Marca!.Value,
            command.Modelo!,
            command.Opcionais,
            command.Valor);

        _veiculoRepository.Atualizar(veiculo);
        await _veiculoRepository.SalvarAlteracoesAsync(cancellationToken);
    }

    public async Task<VeiculoResponse> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var veiculo = await _veiculoRepository.ObterPorIdSomenteLeituraAsync(id, cancellationToken)
            ?? throw new VeiculoNaoEncontradoException();

        return MapearParaResponse(veiculo);
    }

    public async Task<IReadOnlyCollection<VeiculoResponse>> ListarAsync(CancellationToken cancellationToken)
    {
        var veiculos = await _veiculoRepository.ListarAsync(cancellationToken);

        return veiculos
            .Select(MapearParaResponse)
            .ToList();
    }

    public async Task ExcluirAsync(Guid id, CancellationToken cancellationToken)
    {
        var veiculo = await _veiculoRepository.ObterPorIdAsync(id, cancellationToken)
            ?? throw new VeiculoNaoEncontradoException();

        _veiculoRepository.Excluir(veiculo);
        await _veiculoRepository.SalvarAlteracoesAsync(cancellationToken);
    }

    private static VeiculoResponse MapearParaResponse(Veiculo veiculo)
    {
        return new VeiculoResponse(
            veiculo.Id,
            veiculo.Descricao,
            veiculo.Marca,
            veiculo.Modelo,
            veiculo.Opcionais,
            veiculo.Valor);
    }
}
