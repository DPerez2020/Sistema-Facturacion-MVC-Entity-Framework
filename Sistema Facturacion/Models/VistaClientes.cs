using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Facturacion.Models
{
    public class VistaClientes
    {
        public int id { get; set; }
        public string RNC_Cedula { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string categoria { get; set; }
    }
}