﻿using System;
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

        

        public ActionResult listadoProductos()
        {
            //para el  login
            ViewBag.usuario = InicioSesion();
            // SE ENVIA LA LISTA DE PRODUCTOS
            return View(productos());
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

            //para el  login
            ViewBag.usuario = InicioSesion();
            return View(Buscar(id));
        }


        public ActionResult Index()
        {
            //para el  login
            ViewBag.usuario = InicioSesion();
            return View(productos());
        }



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

                return null;

            else

                return (Session["login2"] as Tipo_Usuario).descripcion;

        }


        /*******************************************************/

































    }
}