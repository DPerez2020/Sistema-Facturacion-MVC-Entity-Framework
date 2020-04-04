using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Sistema_Facturacion.Models
{
    public class DBContext:DbContext
    {
        public DBContext():base("SistemaFacturacionConnectionString") { 
        }

       protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entradas>().HasRequired(s => s.Proveedor).WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<Proveedor>().HasRequired(s => s.Productos).WithMany().WillCascadeOnDelete(false);
            //modelBuilder.Entity<Entradas>().HasRequired(s =>s.Producto ).WithMany().WillCascadeOnDelete(false);
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Existencia> Existencias { get; set; }
        public DbSet<Entradas> Entradas { get; set; }

        public System.Data.Entity.DbSet<Sistema_Facturacion.infraestructure.Persona> Personas { get; set; }
    }
}