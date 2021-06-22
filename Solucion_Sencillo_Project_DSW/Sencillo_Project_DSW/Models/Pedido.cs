using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sencillo_Project_DSW.Models
{
    public class Pedido
    {
        public int id_producto { get; set; }
        public string descripcion { get; set; }
        public string medida { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal monto { get { return precio * cantidad; } }
    }
}