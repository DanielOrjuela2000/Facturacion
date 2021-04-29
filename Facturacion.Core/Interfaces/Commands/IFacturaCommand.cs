using Facturacion.Core.Entities.Commands;
using System.Collections.Generic;

namespace Facturacion.Core.Interfaces.Commands
{
    public interface IFacturaCommand
    {
        FacturaCommand SelectById(int id);
        List<FacturaCommand> Select();
        void Insert(FacturaCommand facturaCommand);
        void Update(FacturaCommand facturaCommand);
        void Delete(int id);
    }
}
