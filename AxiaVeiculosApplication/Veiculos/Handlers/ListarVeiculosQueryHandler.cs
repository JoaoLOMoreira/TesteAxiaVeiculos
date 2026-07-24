using AxiaVeiculosApplication.Veiculos.Queries;
using AxiaVeiculosApplication.Veiculos.Responses;
using AxiaVeiculosApplication.Veiculos.Services;
using MediatR;

namespace AxiaVeiculosApplication.Veiculos.Handlers;

public sealed class ListarVeiculosQueryHandler
    : IRequestHandler<ListarVeiculosQuery, IReadOnlyCollection<VeiculoResponse>>
{
    private readonly IVeiculoService _veiculoService;

    public ListarVeiculosQueryHandler(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    public Task<IReadOnlyCollection<VeiculoResponse>> Handle(ListarVeiculosQuery request, CancellationToken cancellationToken)
    {
        return _veiculoService.ListarAsync(cancellationToken);
    }
}
