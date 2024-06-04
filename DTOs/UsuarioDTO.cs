using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicoreriaTom.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string User { get; set; }
        public int RolId { get; set; }
        public int TrabajadorId { get; set; }
    }
}