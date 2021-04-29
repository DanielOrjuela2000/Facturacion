using Facturacion.Core.Entities;
using Facturacion.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDbManager DbManager;
        public ClienteRepository(IDbManager dbManager)
        {
            DbManager = dbManager;
        }
        public Cliente SelectById(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM dbo.tp_cliente");
            sql.AppendLine("WHERE id_cliente = @id_cliente");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_cliente", id));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            var data = DbManager.DbExecute().Rows[0];

            if (data == null)
                return null;

            var cliente = new Cliente();
            cliente.IdCliente = Convert.ToInt32(data["id_cliente"]);
            cliente.Nombre = data["nombre"].ToString();
            cliente.Apellidos = data["apellidos"].ToString();
            cliente.Identificacion = data["identificacion"].ToString();
            cliente.FechaNacimiento = Convert.ToDateTime(data["fecha_nacimiento"]);

            return cliente;
        }
        public IEnumerable<Cliente> Select()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM dbo.tp_cliente");

            DbManager.CreateCommand(sql.ToString(), null, CommandType.Text);
            var data = DbManager.DbExecute();

            if (data == null)
                return null;

            var clientes = new List<Cliente>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                var row = data.Rows[i];
                var cliente = new Cliente
                {
                    IdCliente = Convert.ToInt32(row["id_cliente"]),
                    Nombre =  row["nombre"].ToString(),
                    Apellidos =  row["apellidos"].ToString(),
                    Identificacion =  row["identificacion"].ToString(),
                    FechaNacimiento =  Convert.ToDateTime(row["fecha_nacimiento"])
                };

                clientes.Add(cliente);
            }


            return clientes;
        }
        public void Insert(Cliente cliente)
        {
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO dbo.tp_cliente (nombre, apellidos, identificacion, fecha_nacimiento)");
            sql.AppendLine("VALUES(@nombre, @apellidos, @identificacion, @fecha_nacimiento)");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@nombre", cliente.Nombre));
            parameters.Add(new SqlParameter("@apellidos", cliente.Apellidos));
            parameters.Add(new SqlParameter("@identificacion", cliente.Identificacion));
            parameters.Add(new SqlParameter("@fecha_nacimiento", cliente.FechaNacimiento));


            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            DbManager.ExecuteNonQuery();
        }
        public void Update(Cliente cliente)
        {
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE dbo.tp_cliente");
            sql.AppendLine("SET nombre = @nombre,");
            sql.AppendLine("apellidos = @apellidos,");
            sql.AppendLine("identificacion = @identificacion");
            sql.AppendLine("fecha_nacimiento = @fecha_nacimiento");
            sql.AppendLine("WHERE id_cliente = @id_cliente");


            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@nombre", cliente.Nombre));
            parameters.Add(new SqlParameter("@apellidos", cliente.Apellidos));
            parameters.Add(new SqlParameter("@identificacion", cliente.Identificacion));
            parameters.Add(new SqlParameter("@fecha_nacimiento", cliente.FechaNacimiento));
            parameters.Add(new SqlParameter("@id_cliente", cliente.IdCliente));


            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            DbManager.ExecuteNonQuery();
        }
        public void Delete(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM dbo.tp_cliente");
            sql.AppendLine("WHERE id_cliente = @id_cliente");

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id_cliente", id));

            DbManager.CreateCommand(sql.ToString(), parameters, CommandType.Text);
            DbManager.ExecuteNonQuery();
        }
    }
}
