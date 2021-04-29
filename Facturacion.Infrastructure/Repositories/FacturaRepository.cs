using Facturacion.Core.Entities;
using Facturacion.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Facturacion.Infrastructure.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        #region Propiedades
        private readonly IDbManager DbManager;
        #endregion

        #region Constructores
        public FacturaRepository(IDbManager dbManager)
        {
            DbManager = dbManager;
        }
        #endregion

        public List<Factura> Select()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM dbo.tp_factura");

            DbManager.CreateCommand(sql.ToString(), null, CommandType.Text);
            var data = DbManager.DbExecute();

            if (data == null)
                return null;

            var facturas = new List<Factura>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                var row = data.Rows[i];
                var factura = new Factura
                {
                    IdFactura = Convert.ToInt32(row["id_factura"]),
                    IdCliente = Convert.ToInt32(row["fk_cliente"]),
                    Fecha = Convert.ToDateTime(row["fecha"]),
                };

                if (row["total"] == DBNull.Value)
                    factura.Total = null;
                else
                    factura.Total = Convert.ToInt32(row["total"]);

                facturas.Add(factura);
            }

            return facturas;
        }

        public Factura SelectById(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM dbo.tp_factura");
            sql.AppendLine("WHERE id_factura = @id_factura");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_factura", id));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            var data = DbManager.DbExecute().Rows[0];

            if (data == null)
                return null;

            var factura = new Factura();
            factura.IdFactura = Convert.ToInt32(data["id_factura"]);
            factura.IdCliente = Convert.ToInt32(data["fk_cliente"]);
            factura.Fecha = Convert.ToDateTime(data["fecha"]);
            if (data["total"] == DBNull.Value)
                factura.Total = null;
            else
                factura.Total = Convert.ToInt32(data["total"]);

            return factura;
        }

        public void Insert(Factura factura)
        {
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO dbo.tp_factura (id_factura, fk_cliente, fecha, total)");
            sql.AppendLine("VALUES(@id_factura, @fk_cliente, @fecha, @total)");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_factura", factura.IdFactura));
            parameters.Add(new SqlParameter("@fk_cliente", factura.IdCliente));
            parameters.Add(new SqlParameter("@fecha", factura.Fecha));

            if (factura.Total == null)
                parameters.Add(new SqlParameter("@total", DBNull.Value));
            else
                parameters.Add(new SqlParameter("@total", factura.Total));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            DbManager.ExecuteNonQuery();
        }

        public void Update(Factura factura)
        {
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE dbo.tp_factura");
            sql.AppendLine("SET fk_cliente = @fk_cliente,");
            //sql.AppendLine("fecha = @fecha,");
            //sql.AppendLine("total = @total");
            sql.AppendLine("WHERE id_factura = @id_factura");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@fk_cliente", factura.IdCliente));
            //parameters.Add(new SqlParameter("@fecha", factura.Fecha));
            //parameters.Add(new SqlParameter("@total", factura.Total));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            DbManager.ExecuteNonQuery();
        }

        public void UpdateTotal(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE dbo.tp_factura");
            sql.AppendLine("SET total = (SELECT SUM(precio * cantidad)");
            sql.AppendLine("FROM dbo.tp_factura_detalle");
            sql.AppendLine("WHERE fk_factura = @id_factura)");
            sql.AppendLine("WHERE id_factura = @id_factura");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_factura", id));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            DbManager.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM dbo.tp_factura");
            sql.AppendLine("WHERE id_factura = @id_factura");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_factura", id));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            DbManager.ExecuteNonQuery();
        }

        public int NextId()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT MAX(id_factura) FROM dbo.tp_factura");

            DbManager.CreateCommand(sql.ToString(), null, CommandType.Text);
            return DbManager.DbNextId();
        }
    }
}
