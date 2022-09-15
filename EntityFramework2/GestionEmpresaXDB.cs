using EntityFramework2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFramework2
{
    public class GestionEmpresaXDB : DbContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder opBuilder) {
            if (!opBuilder.IsConfigured) { 
                //TODO llenar con configuraciones
            }
        }

    }
}
