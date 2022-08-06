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

namespace Facturacion.Controllers

{
    public class HomeController : Controller
    {

        Models.DBFacturacionEntities3 modelo = new Models.DBFacturacionEntities3();
        [AutorizarUsuario(idOperacion: 1)]
        public ActionResult Index(string NombreProducto, string oUsuario)
        {
            // aqui va a air los datos de las imagenes
            var ListarProductos = modelo.Productos.ToList();

            if (!string.IsNullOrEmpty(NombreProducto))
            {
                ListarProductos = modelo.Productos.Where(p => p.Nombre_Del_Producto.Contains(NombreProducto)).ToList();
            }

            return View(ListarProductos);
        }

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
        [AutorizarUsuario(idOperacion: 3)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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

        public ActionResult Comprar()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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