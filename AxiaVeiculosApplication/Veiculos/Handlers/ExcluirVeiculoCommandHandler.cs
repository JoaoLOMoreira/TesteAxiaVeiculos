using AxiaVeiculosApplication.Veiculos.Commands;
using AxiaVeiculosApplication.Veiculos.Services;
using MediatR;

namespace AxiaVeiculosApplication.Veiculos.Handlers;

public sealed class ExcluirVeiculoCommandHandler
    : IRequestHandler<ExcluirVeiculoCommand>
{
    private readonly IVeiculoService _veiculoService;

    public ExcluirVeiculoCommandHandler(IVeiculoService veiculoService)
    {
        _veiculoService = veiculoService;
    }

    public Task Handle(ExcluirVeiculoCommand request, CancellationToken cancellationToken)
    {
        return _veiculoService.ExcluirAsync(request.Id, cancellationToken);
    }
}
