using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sistema_Facturacion.Models
{
    public class Producto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe introducir el nombre del producto.")]
        [MaxLength(40,ErrorMessage ="El nombre del producto es demasiado largo.")]
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int ProveedorId { get; set; }
        public List<DetalleFactura> DetalleFacturas { get; set; }
        //public Existencia Existencia { get; set; }
    }
}