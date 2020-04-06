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
    }
}