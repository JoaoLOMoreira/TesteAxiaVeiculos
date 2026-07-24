using AxiaVeiculosDomain.Enumerators;

namespace AxiaVeiculosDomain.Entities;

public sealed class Veiculo
{
    private Veiculo()
    {
    }

    public Veiculo(
        string descricao,
        MarcaVeiculo marca,
        string modelo,
        string? opcionais,
        decimal? valor)
    {
        Id = Guid.NewGuid();
        Atualizar(descricao, marca, modelo, opcionais, valor);
    }

    public Guid Id { get; private set; }

    public string Descricao { get; private set; } = string.Empty;

    public MarcaVeiculo Marca { get; private set; }

    public string Modelo { get; private set; } = string.Empty;

    public string? Opcionais { get; private set; }

    public decimal? Valor { get; private set; }

    public void Atualizar(
        string descricao,
        MarcaVeiculo marca,
        string modelo,
        string? opcionais,
        decimal? valor)
    {
        Descricao = descricao.Trim();
        Marca = marca;
        Modelo = modelo.Trim();
        Opcionais = string.IsNullOrWhiteSpace(opcionais)
            ? null
            : opcionais.Trim();
        Valor = valor;
    }
}
