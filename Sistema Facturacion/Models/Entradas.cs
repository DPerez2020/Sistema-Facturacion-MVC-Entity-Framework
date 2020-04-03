using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Facturacion.Models
{
    public class Entradas
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public int ProveedorId { get; set; }
        public Producto Producto { get; set; }
        public Proveedor Proveedor { get; set; }
    }
}