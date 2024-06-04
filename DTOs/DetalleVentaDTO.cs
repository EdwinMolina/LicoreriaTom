using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicoreriaTom.DTOs
{
    public class DetalleVentaDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Cantidad { get; set; }
        public decimal Total { get; set; }
        public int FinanciamientoId { get; set; }
        public int ProductoId { get; set; }
        public int VentaId { get; set; }
    }
}