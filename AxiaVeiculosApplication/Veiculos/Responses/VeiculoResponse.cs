using AxiaVeiculosDomain.Enumerators;

namespace AxiaVeiculosApplication.Veiculos.Responses;

public sealed record VeiculoResponse(
    Guid Id,
    string Descricao,
    MarcaVeiculo Marca,
    string Modelo,
    string? Opcionais,
    decimal? Valor);
