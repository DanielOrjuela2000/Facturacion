using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Core.Entities
{
    public class FacturaDetalle
    {
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public int IdFacturaDetalle { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
