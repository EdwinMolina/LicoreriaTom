using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LicoreriaTom.DTOs
{
    public class FinanciamientoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string TipoFinanciamiento { get; set; }
    }
}