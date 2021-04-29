using Facturacion.Core.Entities;
using Facturacion.Core.Interfaces;
using Facturacion.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Facturacion.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IDbManager DbManager;
        public ProductoRepository(IDbManager dbManager)
        {
            DbManager = dbManager;
        }
        public Producto SelectById(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM dbo.tc_producto");
            sql.AppendLine("WHERE id_producto = @id_producto");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_producto", id));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            var data = DbManager.DbExecute().Rows[0];

            if (data == null)
                return null;

            var producto = new Producto();
            producto.IdProducto = Convert.ToInt32(data["id_producto"]);
            producto.Nombre = data["nombre"].ToString();
            producto.Precio = Convert.ToDecimal(data["precio"]);
            producto.Existencia = Convert.ToInt32(data["existencia"]);

            return producto;
        }
        public IEnumerable<Producto> Select()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM dbo.tc_producto");

            DbManager.CreateCommand(sql.ToString(), null, CommandType.Text);
            var data = DbManager.DbExecute();

            if (data == null)
                return null;

            var productos = new List<Producto>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                var row = data.Rows[i];
                var producto = new Producto
                {
                    IdProducto = Convert.ToInt32(row["id_producto"]),
                    Nombre = row["nombre"].ToString(),
                    Precio = Convert.ToDecimal(row["precio"]),
                    Existencia = Convert.ToInt32(row["existencia"])
                };

                productos.Add(producto);
            }


            return productos;
        }
        public void Insert(Producto producto)
        {
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO dbo.tc_producto (nombre, precio, existencia)");
            sql.AppendLine("VALUES(@nombre, @precio, @existencia)");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@nombre", producto.Nombre));
            parameters.Add(new SqlParameter("@precio", producto.Precio));
            parameters.Add(new SqlParameter("@existencia", producto.Existencia));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            DbManager.ExecuteNonQuery();
        }
        public void Update(Producto producto)
        {
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE dbo.tc_producto");
            sql.AppendLine("SET nombre = @nombre,");
            sql.AppendLine("precio = @precio,");
            sql.AppendLine("existencia = @existencia");
            sql.AppendLine("WHERE id_producto = @id_producto");


            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@nombre", producto.Nombre));
            parameters.Add(new SqlParameter("@precio", producto.Precio));
            parameters.Add(new SqlParameter("@existencia", producto.Existencia));
            parameters.Add(new SqlParameter("@id_producto", producto.IdProducto));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            DbManager.ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM dbo.tc_producto");
            sql.AppendLine("WHERE id_producto = @id_producto");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_producto", id));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            DbManager.ExecuteNonQuery();
        }
    }
}
