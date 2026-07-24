using AxiaVeiculosApplication.Veiculos.Commands;
using AxiaVeiculosApplication.Veiculos.Responses;
using AxiaVeiculosApplication.Veiculos.Services;
using MediatR;

namespace AxiaVeiculosApplication.Veiculos.Handlers;

public sealed class AdicionarVeiculoCommandHandler
    : IRequestHandler<AdicionarVeiculoCommand, VeiculoResponse>
{
    private readonly IVeiculoService _veiculoService;

    public AdicionarVeiculoCommandHandler(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    public Task<VeiculoResponse> Handle(AdicionarVeiculoCommand request, CancellationToken cancellationToken)
    {
        return _veiculoService.AdicionarAsync(request, cancellationToken);
    }
}
