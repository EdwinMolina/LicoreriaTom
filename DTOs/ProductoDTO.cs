using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicoreriaTom.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int TipoProductoId { get; set; }
        public int CategoriaId { get; set; }
    }
}