//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Facturacion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleFactura
    {
        public int ItemID { get; set; }
        public int FacturaID { get; set; }
        public int ProductoID { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio_Unitario { get; set; }
    
        public virtual Factura Factura { get; set; }
        public virtual Productos Productos { get; set; }
    }
}
