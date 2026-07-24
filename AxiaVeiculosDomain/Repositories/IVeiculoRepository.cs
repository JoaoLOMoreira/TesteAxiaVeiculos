using AxiaVeiculosDomain.Entities;

namespace AxiaVeiculosDomain.Repositories;

public interface IVeiculoRepository
{
    Task AdicionarAsync(Veiculo veiculo, CancellationToken cancellationToken);

    Task<Veiculo?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Veiculo?> ObterPorIdSomenteLeituraAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<Veiculo>> ListarAsync(CancellationToken cancellationToken);

    void Atualizar(Veiculo veiculo);

    void Excluir(Veiculo veiculo);

    Task<int> SalvarAlteracoesAsync(CancellationToken cancellationToken);
}
