using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sencillo_Project_DSW.Controllers;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Sencillo_Project_DSW.Models;

namespace Sencillo_Project_DSW.Controllers
{
    public class HomeController : Controller
    {
        string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
        IEnumerable<Producto> productos()
        {
            List<Producto> temporal = new List<Producto>();
            SqlConnection cn = new SqlConnection(cadena);
            SqlCommand cmd = new SqlCommand(
            "SELECT id_producto, descripcion, marca, precio, stock, medida, estado, id_categoria FROM tb_producto", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Producto reg = new Producto();
                reg.idProducto = dr.GetInt32(0);
                reg.descripcion = dr.GetString(1);
                reg.marca = dr.GetString(2);
                reg.precio = dr.GetDecimal(3);
                reg.stock = dr.GetInt32(4);
                reg.medida = dr.GetString(5);
                reg.estado = dr.GetString(6);
                reg.idCategoria = dr.GetInt32(7);
                temporal.Add(reg);
            }
            dr.Close(); cn.Close();
            return temporal;
        }
        public ActionResult Login()
        {
            return View();
        }





 
    }
}