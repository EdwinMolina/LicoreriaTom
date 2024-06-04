using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicoreriaTom.DTOs
{
    public class VentaDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Fecha { get; set; }
        public int ClienteId { get; set; }
        public int TrabajadorId { get; set; }
    }
}