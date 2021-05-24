using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sencillo_Project_DSW.Models
{
    public class Producto
    {
        public int idProducto { get; set; } 
        public string descripcion { get; set; }
        public string marca { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public string medida { get; set; }
        public string estado { get; set; }
        public int idCategoria { get; set; }
    }
}