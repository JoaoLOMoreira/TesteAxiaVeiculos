using AxiaVeiculosApplication.Veiculos.Responses;
using MediatR;

namespace AxiaVeiculosApplication.Veiculos.Queries;

public sealed record ObterVeiculoPorIdQuery(Guid Id) : IRequest<VeiculoResponse>;
