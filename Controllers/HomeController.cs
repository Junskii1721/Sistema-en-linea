using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web.Security;
using System.Net;
using Facturacion.Filtro;
using Facturacion.Models;

namespace Facturacion.Controllers

{
    public class HomeController : Controller
    {
        Models.DB_ContactoEntities dbContacto = new Models.DB_ContactoEntities();
        Models.DBFacturacionEntities3 modelo = new Models.DBFacturacionEntities3();
        Models.DB_ImagenesEntities1 modeloImg = new Models.DB_ImagenesEntities1();

        [AutorizarUsuario(idOperacion: 1)]
        public ActionResult Index()
        {
            // aqui va a air los datos de las imagenes
            var ListarImagenes = modeloImg.Imagenes.ToList();

            return View(ListarImagenes);
        }

        [AutorizarUsuario(idOperacion: 1)]
        public ActionResult ProductosDisponibles(string NombreProducto, string oUsuario)
        {
            var ListarProductos = modelo.Productos.ToList();

            if (!string.IsNullOrEmpty(NombreProducto))
            {
                ListarProductos = modelo.Productos.Where(p => p.Nombre_Del_Producto.Contains(NombreProducto)).ToList();
            }

            return View(ListarProductos);
        }

        public List<Models.Almacen> GetListarAlmacenes()
        {

            List<Models.Almacen> Almacenes = modelo.Almacen.ToList();

            return Almacenes;
        }

        [HttpGet]
        public ActionResult NuevoProducto()
        {

            ViewBag.ListarAlmacenes = new SelectList(modelo.Almacen, "AlmacenID", "Nombre_De_La_Distribuidora");

            return View();

        }

        [HttpPost]
        public ActionResult NuevoProducto(Models.Productos productos)
        {

            ViewBag.ListarAlmacenes = new SelectList(modelo.Almacen, "AlmacenID", "Nombre_De_La_Distribuidora");

            modelo.Productos.Add(productos);
            modelo.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult EditarProducto(int? id)
        {

            Models.Productos productos = modelo.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }

            ViewBag.ListarAlmacenes = new SelectList(modelo.Almacen, "AlmacenID", "Nombre_De_La_Distribuidora");
            return View(productos);

        }

        [HttpPost]
        public ActionResult EditarProducto(Models.Productos productos)
        {

            ViewBag.ListarAlmacenes = new SelectList(modelo.Almacen, "AlmacenID", "Nombre_De_La_Distribuidora");

            modelo.Entry(productos).State = EntityState.Modified;
            modelo.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult EliminarRegistro(int? id)
        {
            Models.Productos productos = modelo.Productos.Find(id);
            if (productos == null)
            {
                return HttpNotFound();
            }

            ViewBag.ListarAlmacenes = new SelectList(modelo.Almacen, "AlmacenID", "Nombre_De_La_Distribuidora");
            return View(productos);
        }

        [HttpPost, ActionName("EliminarRegistro")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Models.Productos productos = modelo.Productos.Find(id);
            modelo.Productos.Remove(productos);
            modelo.SaveChanges();
            return RedirectToAction("Index");
        }
        [AutorizarUsuario(idOperacion: 2)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // Mostrar o renderizar vista contacto
        [AutorizarUsuario(idOperacion: 3)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Guarde la informacion del contacto.
        [HttpPost]
        public ActionResult guardarMensaje(string Nombre, string Apellido, string Correo, string Celular, string Mensaje)
        {
            try
            {
                Models.contacto oContacto = new Models.contacto();
                oContacto.Nombre = Nombre;
                oContacto.Apellido = Apellido;
                oContacto.NombreCompleto = Nombre + " " + Apellido;
                oContacto.Correo = Correo;
                oContacto.Celular = Celular;
                oContacto.Mensaje = Mensaje;
                oContacto.FechaMensaje = DateTime.Now;

                dbContacto.contacto.Add(oContacto);
                dbContacto.SaveChanges();
                // Redireciona a la vista de enviado con exito.
                return RedirectToAction("Exito", "Home");
            }
            catch (Exception)
            {
                // Redireccionar a la vista fallo al enviar.
                return RedirectToAction("Error", "Home");
            }
        }
        public ActionResult Exito()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Error()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AutorizarUsuario(idOperacion: 4)]
        public ActionResult AgregarUnProducto()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AutorizarUsuario(idOperacion: 5)]
        public ActionResult Inventario(string NombreProducto, string oUsuario)
        {

            var ListarProductos = modelo.Productos.ToList();

            if (!string.IsNullOrEmpty(NombreProducto))
            {
                ListarProductos = modelo.Productos.Where(p => p.Nombre_Del_Producto.Contains(NombreProducto)).ToList();
            }

            return View(ListarProductos);
        }

        // Renderizar vista comprar producto
        // Esta debe recibir un id
        public ActionResult Comprar(int? id)
        {
            try
            {
                // con el id que recibimos, buscamos en la tabla productos el producto con el id.
                // si se encuentra lo convertimos en una lista.
                var detalleProducto = (from p in modelo.Productos
                                       where p.ProductoID == id
                                       select p).ToList();

                // validamos si se encontro el producto.

                if (detalleProducto == null)
                {
                    // si no se encontro el producto retornamos que no se encontro
                    return HttpNotFound();
                }

                // si se encontro el producto retornamos la vista y le enviamos los datos del producto.
                return View(detalleProducto);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // Metodo de Comprar se ejecuta cuando el usuario hace click en el botón comprar.
        [HttpPost]
        public ActionResult FinalizarCompra(int idProducto, int cantidad)
        {
            try
            {
                // crea un objeto con los datos que encuentre con el idproducto
                Models.Productos producto = modelo.Productos.Find(idProducto);

                // reasignamos la cantidad del inventario restandole lo que existe.
                // menos la cantidad vendida al cliente.
                producto.Cantidad_En_Inventario = producto.Cantidad_En_Inventario - Convert.ToDecimal(cantidad);

                //actualizamos tabla de productos
                modelo.Entry(producto).State = EntityState.Modified;
                modelo.SaveChanges();

                // si la transaccion fue exitosa redireccionamos a vista Exito.
                return RedirectToAction("Exito", "Home");
            }
            catch (Exception)
            {
                // si la transaccion fallo redireccionamos a vista Error.
                return RedirectToAction("Error", "Home");
            }

        }

        public ActionResult CerrarSesion()
        {

            Session["User"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Acceso");
        }

        [AutorizarUsuario(idOperacion: 6)]
        public ActionResult Control_Carausel()
        {
            ViewBag.Message = "Esta es la pagina de Control Carausel";

            return View();
        }


        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Productos Producto = modelo.Productos.Find(id);
            if (Producto == null)
            {
                return HttpNotFound();
            }
            return View(Producto);
        }



    }

    public class Dto
    {

        public int ProductoID { get; set; }
        public string Nombre_Del_Producto { get; set; }
        public int Cantidad_En_Inventario { get; set; }
        public double Precio_Estandar { get; set; }
        public string Almacen { get; set; }

    }



}