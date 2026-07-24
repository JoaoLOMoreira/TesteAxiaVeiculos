using AxiaVeiculosDomain.Enumerators;

namespace AxiaVeiculosApplication.Veiculos.Requests;

public sealed record AtualizarVeiculoRequest(
    string? Descricao,
    MarcaVeiculo? Marca,
    string? Modelo,
    string? Opcionais,
    decimal? Valor);
