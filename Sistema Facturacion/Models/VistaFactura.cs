using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Facturacion.Models
{
    public class VistaFactura
    {
        public string NombreProducto { get; set; }
        public decimal PrecioProduco { get; set; }
        public int CantidadProducto { get; set; }
        public decimal Total { get; set; }
        public int Id { get; set; }
        public string Cliente { get; set; }
        public decimal TotalProducto { get; set; }
        public decimal TotalPagado { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleFactura> DetalleFacturas { get; set; }
    }
    public class VistaDetalle {
        public int CantidadProducto { get; set; }
        public decimal Total { get; set; }
        public string Producto { get; set; }
        public decimal Precio { get; set; }
    }
}