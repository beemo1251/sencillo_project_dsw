using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sencillo_Project_DSW.Models
{
    public class Registro
    {
        [Required] public string dni { get; set; }
        [Required] public string nombre { get; set; }
        [Required] public string apellido { get; set; }
        [Required] public string correo { get; set; }
        [Required] public string contraseña { get; set; }
    }
}