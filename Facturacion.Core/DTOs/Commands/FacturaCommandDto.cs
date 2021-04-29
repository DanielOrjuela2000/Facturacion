using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Core.DTOs.Commands
{
    public class FacturaCommandDto
    {
        public FacturaDto Factura { get; set; }
        public List<FacturaDetalleDto> FacturaDetalles { get; set; }
    }
}
