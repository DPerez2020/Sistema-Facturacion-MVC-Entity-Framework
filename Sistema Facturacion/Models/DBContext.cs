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
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<Sistema_Facturacion.infraestructure.Persona> Personas { get; set; }
    }
}