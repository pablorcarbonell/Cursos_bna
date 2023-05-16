using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class TransferenciasContext : DbContext
    {
        public TransferenciasContext(DbContextOptions<TransferenciasContext> options) : base(options) { }

        public DbSet<Transferencia> Transferencias { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
    }
}