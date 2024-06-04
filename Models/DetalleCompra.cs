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
    
    public partial class DetalleCompra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DetalleCompra()
        {
            this.Proveedor = new HashSet<Proveedor>();
        }
    
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public int CompraId { get; set; }
        public int ProductoId { get; set; }
    
        public virtual Compra Compra { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proveedor> Proveedor { get; set; }
        public virtual Producto Producto { get; set; }
    }
}