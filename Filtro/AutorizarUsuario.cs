using Facturacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Facturacion.Filtro
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AutorizarUsuario : AuthorizeAttribute

    {
        private Usuario oUsuario;
        private DB_LogginEntities3 db = new DB_LogginEntities3();
        private int idOperacion;


        public AutorizarUsuario(int idOperacion = 0)
        {
            this.idOperacion = idOperacion;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String nombreOperacion = "";
            String nombreModulo = "";

            try
            {
                oUsuario = (Usuario)HttpContext.Current.Session["User"];
                var lstMisOperaciones = from m in db.Rol_Operacion
                                        where m.idRol == oUsuario.idRol
                                        && m.idOperacion == idOperacion
                                        select m;

                if (lstMisOperaciones.ToList().Count() < 1)
                {
                    var oOperacion = db.Operaciones.Find(idOperacion);
                    int? idModulo = oOperacion.idModulo;
                    nombreOperacion = getNombreDeOperacion(idOperacion);
                    nombreModulo = getNombreDelModulo(idModulo);
                    filterContext.Result = new RedirectResult("~/Error/Index");
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Error/Index");
            }
        }


        public string getNombreDeOperacion(int idOperacion)
        {
            var ope = from op in db.Operaciones
                      where op.id == idOperacion
                      select op.nombre;
            String nombreOperacion;

            try
            {
                nombreOperacion = ope.First();
            }
            catch (Exception)
            {
                nombreOperacion = "";
            }
            return nombreOperacion;
        }

        public string getNombreDelModulo(int? idModulo)
        {
            var modulo = from m in db.Modulo
                         where m.id == idModulo
                         select m.nombre;

            String nombreModulo;

            try
            {
                nombreModulo = modulo.First();
            }
            catch (Exception)
            {
                nombreModulo = "";
            }
            return nombreModulo;
        }
    }
}