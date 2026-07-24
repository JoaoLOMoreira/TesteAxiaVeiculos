using AxiaVeiculosApplication.Veiculos.Queries;
using AxiaVeiculosApplication.Veiculos.Responses;
using AxiaVeiculosApplication.Veiculos.Services;
using MediatR;

namespace AxiaVeiculosApplication.Veiculos.Handlers;

public sealed class ObterVeiculoPorIdQueryHandler
    : IRequestHandler<ObterVeiculoPorIdQuery, VeiculoResponse>
{
    private readonly IVeiculoService _veiculoService;

    public ObterVeiculoPorIdQueryHandler(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    public Task<VeiculoResponse> Handle(ObterVeiculoPorIdQuery request, CancellationToken cancellationToken)
    {
        return _veiculoService.ObterPorIdAsync(request.Id, cancellationToken);
    }
}
