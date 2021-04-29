using Facturacion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Facturacion.Core.Interfaces
{
    public interface IFacturaRepository
    {
        Factura SelectById(int id);
        List<Factura> Select();
        void Insert(Factura factura);
        void Update(Factura factura);
        void UpdateTotal(int id);
        void Delete(int id);
        int NextId();
    }
}
