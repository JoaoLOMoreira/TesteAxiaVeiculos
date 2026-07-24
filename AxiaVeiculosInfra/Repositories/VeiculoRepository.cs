using AxiaVeiculosDomain.Entities;
using AxiaVeiculosDomain.Repositories;
using AxiaVeiculosInfra.Data;
using Microsoft.EntityFrameworkCore;

namespace AxiaVeiculosInfra.Repositories;

public sealed class VeiculoRepository : IVeiculoRepository
{
    private readonly VeiculosDbContext _context;

    public VeiculoRepository(VeiculosDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Veiculo veiculo, CancellationToken cancellationToken)
    {
        await _context.Veiculos.AddAsync(veiculo, cancellationToken);
    }

    public Task<Veiculo?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Veiculos
            .FirstOrDefaultAsync(veiculo => veiculo.Id == id, cancellationToken);
    }

    public Task<Veiculo?> ObterPorIdSomenteLeituraAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Veiculos
            .AsNoTracking()
            .FirstOrDefaultAsync(veiculo => veiculo.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Veiculo>> ListarAsync(CancellationToken cancellationToken)
    {
        return await _context.Veiculos
            .AsNoTracking()
            .OrderBy(veiculo => veiculo.Descricao)
            .ToListAsync(cancellationToken);
    }

    public void Atualizar(Veiculo veiculo)
    {
        _context.Veiculos.Update(veiculo);
    }

    public void Excluir(Veiculo veiculo)
    {
        _context.Veiculos.Remove(veiculo);
    }

    public Task<int> SalvarAlteracoesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
