using AxiaVeiculosApplication.Veiculos.Commands;
using AxiaVeiculosApplication.Veiculos.Responses;

namespace AxiaVeiculosApplication.Veiculos.Services;

public interface IVeiculoService
{
    Task<VeiculoResponse> AdicionarAsync(AdicionarVeiculoCommand command, CancellationToken cancellationToken);

    Task AtualizarAsync(AtualizarVeiculoCommand command, CancellationToken cancellationToken);

    Task<VeiculoResponse> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<VeiculoResponse>> ListarAsync(CancellationToken cancellationToken);

    Task ExcluirAsync(Guid id, CancellationToken cancellationToken);
}
