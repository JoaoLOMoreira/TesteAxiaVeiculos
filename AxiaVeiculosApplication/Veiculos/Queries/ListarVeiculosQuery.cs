using AxiaVeiculosApplication.Veiculos.Responses;
using MediatR;

namespace AxiaVeiculosApplication.Veiculos.Queries;

public sealed record ListarVeiculosQuery : IRequest<IReadOnlyCollection<VeiculoResponse>>;
