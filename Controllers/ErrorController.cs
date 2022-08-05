using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facturacion.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}