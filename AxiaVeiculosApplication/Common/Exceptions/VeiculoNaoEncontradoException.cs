namespace AxiaVeiculosApplication.Common.Exceptions;

public sealed class VeiculoNaoEncontradoException : Exception
{
    public VeiculoNaoEncontradoException()
        : base("Veiculo não encontrado.")
    {
    }
}
