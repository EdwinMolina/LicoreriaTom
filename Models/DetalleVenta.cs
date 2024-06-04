//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LicoreriaTom.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DetalleVenta
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Cantidad { get; set; }
        public decimal Total { get; set; }
        public int FinanciamientoId { get; set; }
        public int ProductoId { get; set; }
        public int VentaId { get; set; }
    
        public virtual Financiamiento Financiamiento { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Venta Venta { get; set; }
    }
}