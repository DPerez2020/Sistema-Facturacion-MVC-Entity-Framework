using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Facturacion.Models
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public int FacturacionId { get; set; }
        public int CantidadProducto { get; set; }
        public int ProductoId { get; set; }
        public decimal Total { get; set; }
        public Producto Producto { get; set; }
    }
}