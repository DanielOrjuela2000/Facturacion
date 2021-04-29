using Facturacion.Core.Entities;
using Facturacion.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Facturacion.Infrastructure.Repositories
{
    public class FacturaDetalleRepository : IFacturaDetalleRepository
    {
        #region Propiedades
        private readonly IDbManager DbManager;
        private readonly IFacturaRepository _facturaRepository;
        #endregion

        #region Constructores
        public FacturaDetalleRepository(IDbManager dbManager, IFacturaRepository facturaRepository)
        {
            DbManager = dbManager;
            _facturaRepository = facturaRepository;
        }
        #endregion

        public List<FacturaDetalle> SelectByIdFactura(int idFactura)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM dbo.tp_factura_detalle");
            sql.AppendLine("WHERE fk_factura = @fk_factura");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fk_factura", idFactura));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            var data = DbManager.DbExecute();

            if (data == null)
                return null;

            var facturasDetalle = new List<FacturaDetalle>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                var row = data.Rows[i];
                var facturaDetalle = new FacturaDetalle
                {
                    IdFactura = Convert.ToInt32(row["fk_factura"]),
                    IdProducto = Convert.ToInt32(row["fk_producto"]),
                    IdFacturaDetalle = Convert.ToInt32(row["id_factura_detalle"]),
                    Precio = Convert.ToDecimal(row["precio"]),
                    Cantidad = Convert.ToInt32(row["cantidad"])
                };

                facturasDetalle.Add(facturaDetalle);
            }

            return facturasDetalle;
        }

        public IEnumerable<FacturaDetalle> SelectByIdFacturaByIdProducto(int idFactura, int idProducto)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM dbo.tp_factura_datalle");
            sql.AppendLine("WHERE fk_factura = @fk_factura");
            sql.AppendLine("AND fk_producto = @fk_producto");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fk_factura", idFactura));
            parameters.Add(new SqlParameter("@fk_producto", idProducto));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            var data = DbManager.DbExecute();

            if (data == null)
                return null;

            var facturasDetalle = new List<FacturaDetalle>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                var row = data.Rows[i];
                var facturaDetalle = new FacturaDetalle
                {
                    IdFactura = Convert.ToInt32(row["fk_factura"]),
                    IdProducto = Convert.ToInt32(row["fk_producto"]),
                    IdFacturaDetalle = Convert.ToInt32(row["id_factura_detalle"]),
                    Precio = Convert.ToDecimal(row["precio"]),
                    Cantidad = Convert.ToInt32(row["total"])
                };

                facturasDetalle.Add(facturaDetalle);
            }

            return facturasDetalle;
        }

        public FacturaDetalle SelectByIdFacturaByIdProductoById(int idFactura, int idProducto, int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM dbo.tp_factura_detalle");
            sql.AppendLine("WHERE fk_factura = @fk_factura");
            sql.AppendLine("AND fk_producto = @fk_producto");
            sql.AppendLine("AND id_factura_detalle = @id_factura_detalle");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fk_factura", idFactura));
            parameters.Add(new SqlParameter("@fk_producto", idProducto));
            parameters.Add(new SqlParameter("@id_factura_detalle", id));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            var data = DbManager.DbExecute().Rows[0];

            if (data == null)
                return null;

            var facturaDetalle = new FacturaDetalle
            {
                IdFactura = Convert.ToInt32(data["fk_factura"]),
                IdProducto = Convert.ToInt32(data["fk_producto"]),
                IdFacturaDetalle = Convert.ToInt32(data["id_factura_detalle"]),
                Precio = Convert.ToDecimal(data["precio"]),
                Cantidad = Convert.ToInt32(data["total"])
            };

            return facturaDetalle;
        }

        public void Insert(FacturaDetalle facturaDetalle)
        {
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO dbo.tp_factura_detalle (fk_factura, fk_producto, id_factura_detalle, precio, cantidad)");
            sql.AppendLine("VALUES(@fk_factura, @fk_producto, @id_factura_detalle, @precio, @cantidad)");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fk_factura", facturaDetalle.IdFactura));
            parameters.Add(new SqlParameter("@fk_producto", facturaDetalle.IdProducto));
            parameters.Add(new SqlParameter("@id_factura_detalle", facturaDetalle.IdFacturaDetalle));
            parameters.Add(new SqlParameter("@precio", facturaDetalle.Precio));
            parameters.Add(new SqlParameter("@cantidad", facturaDetalle.Cantidad));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            DbManager.ExecuteNonQuery();

            _facturaRepository.UpdateTotal(facturaDetalle.IdFactura);
        }

        public void Update(FacturaDetalle facturaDetalle)
        {
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE dbo.tp_factura_detalle");
            sql.AppendLine("SET precio = @precio,");
            sql.AppendLine("cantidad = @cantidad");
            sql.AppendLine("WHERE fk_factura = @fk_factura");
            sql.AppendLine("AND fk_producto = @fk_producto");
            sql.AppendLine("AND id_factura_detalle = @id_factura_detalle");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@precio", facturaDetalle.Precio));
            parameters.Add(new SqlParameter("@cantidad", facturaDetalle.Cantidad));
            parameters.Add(new SqlParameter("@fk_factura", facturaDetalle.IdFactura));
            parameters.Add(new SqlParameter("@fk_producto", facturaDetalle.IdProducto));
            parameters.Add(new SqlParameter("@id_factura_detalle", facturaDetalle.IdFacturaDetalle));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            DbManager.ExecuteNonQuery();
        }

        public void Delete(int idFactura, int idProducto, int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM dbo.tp_factura_detalle");
            sql.AppendLine("WHERE fk_factura = @fk_factura");
            sql.AppendLine("AND fk_producto = @fk_producto");
            sql.AppendLine("AND id_factura_detalle = @id_factura_detalle");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fk_factura", idFactura));
            parameters.Add(new SqlParameter("@fk_producto", idProducto));
            parameters.Add(new SqlParameter("@id_factura_detalle", id));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            DbManager.ExecuteNonQuery();
        }

        public int NextId(int idFactura, int idProducto)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT MAX(id_factura_detalle) FROM dbo.tp_factura_detalle");
            sql.AppendLine("WHERE fk_factura = @fk_factura AND fk_producto = @fk_producto");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fk_factura", idFactura));
            parameters.Add(new SqlParameter("@fk_producto", idProducto));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            return DbManager.DbNextId();
        }
    }
}
