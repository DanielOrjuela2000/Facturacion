using Facturacion.Core.Entities;
using System.Collections.Generic;

namespace Facturacion.Core.Interfaces
{
    public interface IProductoRepository
    {
        Producto SelectById(int id);
        IEnumerable<Producto> Select();
        void Insert(Producto producto);
        void Update(Producto producto);
        void Delete(int id);
    }
}
