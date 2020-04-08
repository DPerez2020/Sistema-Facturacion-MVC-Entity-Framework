using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Sistema_Facturacion.infraestructure
{
    public abstract class Persona
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Debe introducir el RNC / Cedula")]
        [MaxLength(11,ErrorMessage ="El RNC / Cedula no es valido")]
        [RegularExpression("[0-9]{11}", ErrorMessage ="El RNC / Cedula no es valido")]
        public string RNC_Cedula { get; set; }
        [Required(ErrorMessage = "Debe introducir el nombre")]
        [StringLength(80,ErrorMessage ="El nombre es demasiado largo")]
        [MinLength(3,ErrorMessage ="El nombre es demasiado corto")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe introducir el numero telefonico")]
        [MaxLength(10,ErrorMessage ="El telefono  no es valido")]
        [RegularExpression("[8][0,2,4][9][0-9]{7}", ErrorMessage = "El telefono no es valido")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Debe introducir el correo electronico")]
        [MaxLength(80,ErrorMessage ="El email es demasiado largo")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage ="El email no es valido")]
        public string Email { get; set; }
    }
}