using AxiaVeiculosDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AxiaVeiculosInfra.Data;

public sealed class VeiculosDbContext : DbContext
{
    public VeiculosDbContext(DbContextOptions<VeiculosDbContext> options)
        : base(options)
    {
    }

    public DbSet<Veiculo> Veiculos => Set<Veiculo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var veiculo = modelBuilder.Entity<Veiculo>();

        veiculo.HasKey(entity => entity.Id);

        veiculo.Property(entity => entity.Descricao)
            .IsRequired()
            .HasMaxLength(200);

        veiculo.Property(entity => entity.Marca)
            .IsRequired();

        veiculo.Property(entity => entity.Modelo)
            .IsRequired()
            .HasMaxLength(100);

        veiculo.Property(entity => entity.Opcionais)
            .HasMaxLength(500);
    }
}
