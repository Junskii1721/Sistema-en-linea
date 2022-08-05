using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loggin.Dto
{
    public class Usuario
    {

        public int IdUsuario { get; set; }

        public string correo { get; set; }

        public string Contraseña { get; set; }

        public string ConfirmarContraseña { get; set; }

   
    }



}