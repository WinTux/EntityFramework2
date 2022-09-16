using EntityFramework2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EntityFramework2
{
    public class GestionEmpresaXDB : DbContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder opBuilder) {
            if (!opBuilder.IsConfigured) {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appconfig.json");
                var conf = builder.Build();
                opBuilder.UseLazyLoadingProxies().UseSqlServer(conf["ConnectionStrings:miConexion"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Telefono>(entidad => {
                entidad.HasOne(t => t.estudiante).WithMany(est => est.telefonos)
                .HasForeignKey(t => t.codigoEst)
                .HasConstraintName("FK_Telefono_Estudiante");
            });
        }
    }
}
