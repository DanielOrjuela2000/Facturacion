using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Core.DTOs
{
    public class ClienteDto
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Identificacion { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
