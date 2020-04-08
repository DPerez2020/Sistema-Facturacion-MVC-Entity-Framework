using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Facturacion.Models
{
    public class Categoria
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Debe introducir una descripcion.")]
        [StringLength(30,ErrorMessage ="La longitud maxima es de 30 caracteres")]
        public string Descripcion { get; set; }
        public List<Cliente> Clientes { get; set; }
    }
}