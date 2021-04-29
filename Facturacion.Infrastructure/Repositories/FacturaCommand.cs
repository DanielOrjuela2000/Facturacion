using Facturacion.Core.Entities;
using Facturacion.Core.Interfaces;
using Facturacion.Core.Interfaces.Commands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Facturacion.Infrastructure.Repositories
{
    public class FacturaCommand : IFacturaCommand
    {
        #region Propiedades
        private readonly IDbManager DbManager;
        private readonly IFacturaRepository _facturaRepository;
        private readonly IFacturaDetalleRepository _facturaDetalleRepository;
        #endregion

        #region Constructores
        public FacturaCommand(IDbManager dbManager, IFacturaRepository facturaRepository, IFacturaDetalleRepository facturaDetalleRepository)
        {
            DbManager = dbManager;
            _facturaRepository = facturaRepository;
            _facturaDetalleRepository = facturaDetalleRepository;
        }
        #endregion

        public void Insert(Core.Entities.Commands.FacturaCommand facturaCommand)
        {
            try
            {
                //Generar nuevo IdFactura
                DbManager.TransactionBegin();

                var newIdFactura = _facturaRepository.NextId();
                facturaCommand.Factura.IdFactura = newIdFactura;
                facturaCommand.Factura.Fecha = DateTime.Now;

                //Insertar Factura
                _facturaRepository.Insert(facturaCommand.Factura);

                //Comprobar si hay detalle
                if (facturaCommand.FacturaDetalles != null && facturaCommand.FacturaDetalles.Count > 0)
                {
                    //Detalle
                    foreach (var detalle in facturaCommand.FacturaDetalles)
                    {
                        //Generar nuevo IdFacturaDetalle
                        var newIdFactucturaDetalle = _facturaDetalleRepository.NextId(newIdFactura, detalle.IdProducto);
                        detalle.IdFactura = newIdFactura;
                        detalle.IdFacturaDetalle = newIdFactucturaDetalle;

                        //Insertar Detalle
                        _facturaDetalleRepository.Insert(detalle);
                    }
                }

                ////Actualizar total factura
                //_facturaRepository.UpdateTotal(newIdFactura);

                DbManager.TransactionCommit();
            }
            catch (Exception ex)
            {
                DbManager.TransactionRollback();
                throw new Exception(ex.Message);
            }
        }

        public List<Core.Entities.Commands.FacturaCommand> Select()
        {
            var facturasCommand = new List<Core.Entities.Commands.FacturaCommand>();
            var facturas = _facturaRepository.Select();
            if (facturas == null)
                return null;

            foreach (var factura in facturas)
            {
                var facturaDetalles = _facturaDetalleRepository.SelectByIdFactura(factura.IdFactura);
                facturasCommand.Add(new Core.Entities.Commands.FacturaCommand
                {
                    Factura = factura,
                    FacturaDetalles = facturaDetalles
                });
            }

            return facturasCommand;
        }

        public Core.Entities.Commands.FacturaCommand SelectById(int id)
        {
            var factura = _facturaRepository.SelectById(id);
            if (factura == null)
                return null;

            var facturaDetalles = _facturaDetalleRepository.SelectByIdFactura(id);
            var facturaCommand = new Core.Entities.Commands.FacturaCommand();
            facturaCommand.Factura = factura;
            facturaCommand.FacturaDetalles = facturaDetalles;
            return facturaCommand;
        }

        public void Update(Core.Entities.Commands.FacturaCommand facturaCommand)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            try
            {
                DbManager.TransactionBegin();
                //Eliminar Detalle Factura
                var sql = new StringBuilder();
                sql.AppendLine("DELETE FROM dbo.tp_factura_detalle");
                sql.AppendLine("WHERE fk_factura = @fk_factura");

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@fk_factura", id));

                DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
                DbManager.ExecuteNonQuery();

                //Eliminar Factura
                _facturaRepository.Delete(id);
                DbManager.TransactionCommit();
            }
            catch (Exception ex)
            {
                DbManager.TransactionRollback();
                throw new Exception(ex.Message);
            }

        }
    }
}
