using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Sencillo_Project_DSW.Models;

namespace Sencillo_Project_DSW.Controllers
{
    public class ProductosController : Controller
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

        Producto Buscar(int? id = null)
        {
            if (id == null)
                return new Producto();
            else
                return productos().Where(p => p.idProducto == id).FirstOrDefault();

        }

        public ActionResult listadoProductos()
        {
            // SE ENVIA LA LISTA DE PRODUCTOS
            return View(productos());
        }

        public ActionResult detailsProducto(int? id = null)
        {
            if (id == null) return RedirectToAction("listadoProducto");
            return View(Buscar(id));
        }
    }
}