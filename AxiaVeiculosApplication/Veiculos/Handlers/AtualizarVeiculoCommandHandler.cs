using AxiaVeiculosApplication.Veiculos.Commands;
using AxiaVeiculosApplication.Veiculos.Services;
using MediatR;

namespace AxiaVeiculosApplication.Veiculos.Handlers;

public sealed class AtualizarVeiculoCommandHandler
    : IRequestHandler<AtualizarVeiculoCommand>
{
    private readonly IVeiculoService _veiculoService;

    public AtualizarVeiculoCommandHandler(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    public Task Handle(AtualizarVeiculoCommand request, CancellationToken cancellationToken)
    {
        return _veiculoService.AtualizarAsync(request, cancellationToken);
    }
}
