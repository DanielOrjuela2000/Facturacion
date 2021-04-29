using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Core.Entities.Commands
{
    public class FacturaCommand
    {
        public Factura Factura { get; set; }
        public List<FacturaDetalle> FacturaDetalles { get; set; }
    }
}
