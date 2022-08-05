using Facturacion.Models;
using Loggin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facturacion.Filtro
{
    public class VerificarSession : ActionFilterAttribute
    {
        private Usuario oUser;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                oUser = (Usuario)HttpContext.Current.Session["User"];
                if (oUser == null)
                {
                    if (filterContext.Controller is AccesoController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Acceso/Login");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");
            }
        }
    }

}