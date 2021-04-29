using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Core.Entities
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Existencia { get; set; }
    }
}
