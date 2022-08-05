using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Loggin.Dto;
using Facturacion.Models;
using System.Web.Security;
using Facturacion.Tools;

namespace Loggin.Controllers
{
    public class AccesoController : Controller
    {
     
        DB_LogginEntities3 db = new DB_LogginEntities3();

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Dto.Usuario oUsuario)
        {
       

            bool registrado;
            string mensaje;

            if (oUsuario.Contraseña == oUsuario.ConfirmarContraseña)
            {


                try
                {
                    oUsuario.Contraseña = ConvertirSha256(oUsuario.Contraseña);
                    db.sp_RegistrarUsuario(oUsuario.correo, oUsuario.Contraseña);
                    registrado = true;
                    mensaje = "Usuario registrado";
                }
                catch (Exception error)
                {

                    registrado = false;
                    mensaje = error.Message;
                }
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden ";
                return View();
            }


            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }

            else
            {
                return View();
            }

        }


        [HttpPost]
         public ActionResult Login(string Correo, string Contraseña) 
        {

            try
            {
                // Abrimos conexión a nuestra base de datos
                using (DB_LogginEntities3 db = new DB_LogginEntities3())
                {
                    // Encriptamos la contraseña que el usuario escribio antes de realizar proceso de autenticación.
                    Contraseña = Encriptacion.GetSHA256(Contraseña);
                    // Declaramos variale que captura los datos de los input
                    var oUser = (from d in db.Usuario
                                 where d.Correo == Correo.Trim() && d.Contraseña == Contraseña.Trim()
                                 select d).FirstOrDefault();

                    // Validamos la información
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o contraseña incorrecta";
                        return View();
                    }

                    // Creamos el filtro para bloquear el acceso a las demas páginas
                    Session["User"] = oUser;

                }
                // Si la autenticación del usuario es correcta, redireccionamos a la página de principal.
                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                // En caso de error mandamos un mensaje con el error
                ViewBag.Error = ex.Message;
                return View();
            }



            //            oUsuario.Contraseña = ConvertirSha256(oUsuario.Contraseña);
            //        var user = db.Usuario.Where(x => x.Correo == oUsuario.correo).Where(c => c.Contraseña == oUsuario.Contraseña).FirstOrDefault();
            //            if (user != null)
            //            {
            //                Session["Usuario"] = oUsuario;
            //                return RedirectToAction("Index", "Home");

            //    }
            //            else
            //            {
            //                ViewData["Mensaje"] = "Usuario no encontrado";
            //                return View();
            //}



        }



        public static string ConvertirSha256(string texto)
        {


            //using System.Text;
            //using System.Security.Cryptography;

            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())

            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));

            }
            return Sb.ToString();



        }


    }
}