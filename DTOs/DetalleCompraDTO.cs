using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicoreriaTom.DTOs
{
    public class DetalleCompraDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public int CompraId { get; set; }
        public int ProductoId { get; set; }
    }
}