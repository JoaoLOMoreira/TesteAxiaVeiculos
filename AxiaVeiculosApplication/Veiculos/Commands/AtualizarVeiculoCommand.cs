using AxiaVeiculosDomain.Enumerators;
using MediatR;

namespace AxiaVeiculosApplication.Veiculos.Commands;

public sealed record AtualizarVeiculoCommand(
    Guid Id,
    string? Descricao,
    MarcaVeiculo? Marca,
    string? Modelo,
    string? Opcionais,
    decimal? Valor) : IRequest;
