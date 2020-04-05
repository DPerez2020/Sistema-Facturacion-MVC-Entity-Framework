using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sistema_Facturacion.infraestructure;

namespace Sistema_Facturacion.Models
{
    public class Cliente:Persona
    {
        public int CategoriaId  { get; set; }
        public List<Facturacion> Facturacions { get; set; }
    }
}