using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Sencillo_Project_DSW.Models;

namespace Sencillo_Project_DSW.Controllers
{
    public class RegistroController : Controller
    {
        string cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

        public ActionResult Registro()
        {
            return View(new Registro());
        }

        [HttpPost]
        public ActionResult Registro(Registro reg)
        {
            if (!ModelState.IsValid) return View(reg);

            SqlConnection cn = new SqlConnection(cadena);
            try
            {
                SqlCommand cmd = new SqlCommand("sp_registro_cliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dni", reg.dni);
                cmd.Parameters.AddWithValue("@nombre", reg.nombre);
                cmd.Parameters.AddWithValue("@apellido", reg.apellido);
                cmd.Parameters.AddWithValue("@email", reg.correo);
                cmd.Parameters.AddWithValue("@contraseña", reg.contraseña);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                ViewBag.mensaje = ex.Message;
            }
            finally
            {
                cn.Close();
            }
            return View(reg);
        }
    }
}