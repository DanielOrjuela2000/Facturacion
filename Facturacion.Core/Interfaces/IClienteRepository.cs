using Facturacion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Core.Interfaces
{
    public interface IClienteRepository
    {
        Cliente SelectById(int id);
        IEnumerable<Cliente> Select();
        void Insert(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(int id);
    }
}
