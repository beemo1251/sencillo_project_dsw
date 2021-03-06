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

        

        IEnumerable<Producto> productos(string desc = "")
        {
            List<Producto> temporal = new List<Producto>();
            SqlConnection cn = new SqlConnection(cadena);
            SqlCommand cmd = new SqlCommand("sp_listar_prod", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@descrip", desc);

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

        IEnumerable<Producto> filtro(string nombre = null)
        {
            if (nombre == null)
                return new List<Producto>();
            else
                return productos().Where(p => p.descripcion.StartsWith(nombre,
                    StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public ActionResult Index(string nombre = "", int p = 0, string flecha = "")
        {
            //para el  login
            ViewBag.usuario = InicioSesion();

            if(Session["carrito"] == null)
            {
                Session["carrito"] = new List<Pedido>();
                ViewBag.carrito = "null";
            }

            ViewBag.carrito = "carrito";

            int f = 8;
            int c = productos(nombre).Count();
            int npags = c % f == 0 ? c / f : c / f + 1;

            ViewBag.p = p;
            ViewBag.npags = npags;

            if (flecha == ">>")
            {
                ViewBag.p = p + 1;
            }
            else if (flecha == "<<")
            {
                ViewBag.p = p - 1;
            }

            // SE ENVIA LA LISTA DE PRODUCTOS
            return View(productos(nombre).Skip(p * f).Take(f));
        }

        Producto Buscar(int? id = null)
        {
            if (id == null)
                return new Producto();
            else
                return productos().Where(p => p.idProducto == id).FirstOrDefault();

        }

        public ActionResult detailsProducto(int? id = null)
        {
            if (id == null) return RedirectToAction("Index");

            if (Session["carrito"] == null)
            {
                Session["carrito"] = new List<Pedido>();
                ViewBag.carrito = "null";
            }

            ViewBag.carrito = "carrito";

            //para el  login
            ViewBag.usuario = InicioSesion();
            return View(Buscar(id));
        }

        /*public ActionResult Index()
        {
            //para el  login
            ViewBag.usuario = InicioSesion();
            return View(productos());
        }*/

        /*OPERACIONES PARA EL  FUNCIONAMIENTO DEL  LOGIN*/

        Tipo_Usuario Buscar(string email, string clave)
        {
            //buscar al cliente ejecutando sp_logueo

            Tipo_Usuario reg = null;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_logueo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@contraseña", clave);
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {


                    reg = new Tipo_Usuario()
                    {
                        //id_tipo = dr["id_tipo"].ToString(),

                        descripcion = dr["descripcion"].ToString()
                    };

                }
                dr.Close(); cn.Close();
            }
            return reg;
        }

        public ActionResult Inicio() // creamos  vista  ,  plantilla :empty(sin modelo)
        {
            return View();

        }

        [HttpPost]
        public ActionResult Inicio(string email, string clave)
        {

            Tipo_Usuario reg = Buscar(email, clave);
            if (reg == null)
            {
                ViewBag.mensaje = "Usuario o clave Incorrecta";
                return View();
            }
            else
            {
                Session["login2"] = reg;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Cerrar()
        {
            Session["login2"] = null;
            return RedirectToAction("Index");
        }

        string InicioSesion()
        {
            if (Session["login2"] == null)
            {
                return null;
            }
            else 
            {
                return (Session["login2"] as Tipo_Usuario).descripcion;
            }
        }

        /*******************************************************/
        
        [HttpPost]public ActionResult DetailsProducto(int id, int cantidad, int stock)
        {
            ViewBag.usuario = InicioSesion();

            if (Session["carrito"] == null)
            {
                Session["carrito"] = new List<Pedido>();
                ViewBag.carrito = "null";
            }

            ViewBag.carrito = "carrito";

            if (cantidad > stock)
            {
                ViewBag.mensaje = "Ingrese una cantidad menor al stock";
                return View(Buscar(id));
            }

            //Session["carrito"] = new List<Pedido>();

            Producto reg = Buscar(id);

            Pedido it = new Pedido()
            {
                id_producto = reg.idProducto,
                descripcion = reg.descripcion,
                medida = reg.medida,
                precio = reg.precio,
                cantidad = cantidad,
            };
            List<Pedido> temporal = (List<Pedido>)Session["carrito"];
            temporal.Add(it);

            ViewBag.mensaje = "Producto agregado";
            return View(reg);
        }

        public ActionResult carrito()
        {
            ViewBag.usuario = InicioSesion();
            // Session["boleta"] = new List<Pedido>();

            return View((List<Pedido>)Session["carrito"]);
        }

        public ActionResult Delete(int id)
        {
            List<Pedido> temporal = (List<Pedido>)Session["carrito"];

            Pedido reg = temporal.Find(i => i.id_producto == id);
            temporal.Remove(reg);

            return RedirectToAction("carrito");
        }

        [HttpPost]public ActionResult carrito(string mns)
        {
            if (Session["login2"] == null)
            {
                ViewBag.mensaje = "Debe iniciar sesion para realizar una compra";
                return View((List<Pedido>)Session["carrito"]);
            }

            Session["boleta"] = null;

            Session["boleta"] = new List<Pedido>();

            Session["boleta"] = (List<Pedido>)Session["carrito"];

            // Session["login2"] = null;
            Session["carrito"] = null;

            return RedirectToAction("boleta");
        }

        public ActionResult boleta()
        {
            // Session["boleta"] = new List<Pedido>();
            Session["carrito"] = new List<Pedido>();

            ViewBag.usuario = InicioSesion();

            ViewBag.fecha = DateTime.Now.ToString("dd-MM-yyyy");
            ViewBag.hora = DateTime.Now.ToString("hh:mm:ss");

            return View((List<Pedido>)Session["boleta"]);
        }
    }
}