using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Core.DTOs
{
    public class ProductoDto
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Existencia { get; set; }
    }
}
