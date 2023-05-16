using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class ClienteDb : DbContext
    {
        public ClienteDb(DbContextOptions<ClienteDb> options) : base(options)
        { }

        //public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Cliente> Clientes { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //esto es para cuando se crea la tabla por entity no se cree en plural
        //    modelBuilder.Entity<Cliente>().ToTable("Cliente");
        //}
    }
}
