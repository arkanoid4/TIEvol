using Microsoft.EntityFrameworkCore;
using TIEvol.Shared.Entities;

namespace TIEvol.Server.Data
{
    public class ApplicationDataContext : DbContext
    {
        // Constructor
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
            // 
        }

        // Mapear la base de datos

        public DbSet<Bodega> Bodegas { get; set; }

        public DbSet<Ciudad> Ciudades { get; set; }

        public DbSet<Comuna> Comunas { get; set; }

        public DbSet<Sucursal> Sucursales { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Ciudad>().HasData(new Ciudad { Id = 1, Nombre = "Santiago" });

            //builder.Entity<Ciudad>().HasData(new Ciudad { Id = 2, Nombre = "Antofagasta" });
        }
    }
}
