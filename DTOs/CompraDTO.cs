using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicoreriaTom.DTOs
{
    public class CompraDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public System.DateTime Fecha { get; set; }
        public int TrabajadorId { get; set; }
    }
}