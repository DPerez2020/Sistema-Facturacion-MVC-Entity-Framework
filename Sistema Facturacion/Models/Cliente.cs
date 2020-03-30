using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sistema_Facturacion.infraestructure;

namespace Sistema_Facturacion.Models
{
    public class Cliente:Persona
    {
        public string  Categoria { get; set; }
        public Categoria CategoriaId  { get; set; }
    }
}