using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Facturacion.Models
{
    public class Facturacion
    {
        public int Id { get; set; }
        public int Id_Cliente { get; set; }
        public decimal TotalProducto { get; set; }
        public decimal TotalPagado { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleFactura> DetalleFacturas { get; set; }
    }
}