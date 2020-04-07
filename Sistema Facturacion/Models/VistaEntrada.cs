using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Facturacion.Models
{
    public class VistaEntrada
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string Proveedor { get; set; }
        public string Producto { get; set; }
        public decimal precio { get; set; }
    }
}