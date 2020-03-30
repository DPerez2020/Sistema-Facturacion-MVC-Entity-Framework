using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sistema_Facturacion.infraestructure;

namespace Sistema_Facturacion.Models
{
    public class Cliente:Persona
    {
        //public string  CategoriaId { get; set; }
        public Categoria Categoria  { get; set; }
    }
}