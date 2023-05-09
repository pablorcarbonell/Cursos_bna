using Common.DB;
using Microsoft.EntityFrameworkCore;

namespace ServicioTransferencia
{
    public class TransferenciasDb : DbContext
    {
        public TransferenciasDb(DbContextOptions<TransferenciasDb> options) : base(options)
        { }

        public DbSet<Transferencia> Transferencias { get; set; }
        public DbSet<Transferencia> Respuestas { get; set; }
    }
}

