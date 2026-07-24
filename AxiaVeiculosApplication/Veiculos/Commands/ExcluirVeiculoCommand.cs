using MediatR;

namespace AxiaVeiculosApplication.Veiculos.Commands;

public sealed record ExcluirVeiculoCommand(Guid Id) : IRequest;
