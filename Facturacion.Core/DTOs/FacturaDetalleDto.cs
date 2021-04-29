using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Core.DTOs
{
    public class FacturaDetalleDto
    {
        public int IdProducto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
