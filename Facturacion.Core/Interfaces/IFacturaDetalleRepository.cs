using Facturacion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Core.Interfaces
{
    public interface IFacturaDetalleRepository
    {
        FacturaDetalle SelectByIdFacturaByIdProductoById(int idFactura, int idProducto, int id);
        IEnumerable<FacturaDetalle> SelectByIdFacturaByIdProducto(int idFactura, int idProducto);
        List<FacturaDetalle> SelectByIdFactura(int idFactura);
        void Insert(FacturaDetalle facturaDetalle);
        void Update(FacturaDetalle facturaDetalle);
        void Delete(int idFactura, int idProducto, int id);
        int NextId(int idFactura, int idProducto);
    }
}
