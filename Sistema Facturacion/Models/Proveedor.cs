﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sistema_Facturacion.infraestructure;

namespace Sistema_Facturacion.Models
{
    public class Proveedor:Persona
    {
        public List<Producto> Productos { get; set; }
    }
}