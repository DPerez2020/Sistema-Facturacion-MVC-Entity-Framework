using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Facturacion.Models
{
    public class VistaProductos
    {
        public int id { get; set; }
        public string producto { get; set; }
        public decimal Precio { get; set; }
        public string proveedor { get; set; }
    }
}