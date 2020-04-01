﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Facturacion.Models
{
    public class Categoria
    {
        public int? Id { get; set; }
        public string Descripcion { get; set; }
        public List<Cliente> Clientes { get; set; }
    }
}