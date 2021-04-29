using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Core.Entities
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? Total { get; set; }
    }
}
