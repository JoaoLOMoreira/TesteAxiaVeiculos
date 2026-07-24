using AxiaVeiculosApplication.Veiculos.Responses;
using AxiaVeiculosDomain.Enumerators;
using MediatR;

namespace AxiaVeiculosApplication.Veiculos.Commands;

public sealed record AdicionarVeiculoCommand(
    string? Descricao,
    MarcaVeiculo? Marca,
    string? Modelo,
    string? Opcionais,
    decimal? Valor) : IRequest<VeiculoResponse>;
