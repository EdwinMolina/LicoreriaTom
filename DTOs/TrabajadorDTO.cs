using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicoreriaTom.DTOs
{
    public class TrabajadorDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
    }
}