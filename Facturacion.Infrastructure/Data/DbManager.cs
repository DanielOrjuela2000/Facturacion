using Facturacion.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Facturacion.Infrastructure.Data
{
    public class DbManager : IDbManager, IDisposable
    {
        #region Propiedades
        private string _connectionString;
        private SqlConnection _connection;
        private SqlCommand _command;
        public SqlTransaction Transaction { get; private set; }
        public bool ExistsTransaction => Transaction != null;
        #endregion

        #region Constructores
        public DbManager(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
            Connect();
        }

        ~DbManager()
        {
            Dispose();
        }
        #endregion

        #region Implementacion IDisposable
        public void Dispose()
        {
            Disconnect();
            //_connection = null;
            //_command = null;
            //Transaction = null;
        }
        #endregion

        #region Metodos
        public void Connect()
        {
            _connection.Open();
            Transaction = null;
        }

        public void Disconnect()
        {
            if (ExistsTransaction)
                TransactionRollback();

            if (_connection.State != ConnectionState.Closed)
                _connection.Close();

            //_connection?.Close();
        }

        public void TransactionBegin()
        {
            if (!ExistsTransaction)
                Transaction = _connection.BeginTransaction();
            else
                throw new Exception("Ya existe una transacción activa");
        }

        public void TransactionBegin(IsolationLevel isolationLevel)
        {
            if (!ExistsTransaction)
                Transaction = _connection.BeginTransaction(isolationLevel);
            else
                throw new Exception("Ya existe una transacción activa");
        }

        public void TransactionCommit()
        {
            if (!ExistsTransaction)
                throw new Exception("No existe una transacción activa");

            Transaction.Commit();
            Transaction = null;
        }

        public void TransactionRollback()
        {
            if (!ExistsTransaction) return;

            Transaction.Rollback();
            Transaction = null;
        }

        public void CreateCommand(string sql, List<SqlParameter> parameters, CommandType type)
        {

            _command = _connection.CreateCommand();
            _command.CommandTimeout = 0;
            _command.CommandText = sql;
            switch (type)
            {
                case CommandType.StoredProcedure:
                    _command.CommandType = CommandType.StoredProcedure;
                    break;
                case CommandType.TableDirect:
                    _command.CommandType = CommandType.TableDirect;
                    break;
                case CommandType.Text:
                    _command.CommandType = CommandType.Text;
                    break;
                default:
                    _command.CommandType = CommandType.Text;
                    break;
            }

            if (Transaction != null)
                _command.Transaction = Transaction;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    _command.Parameters.Add(parameter);
                }
            }
        }

        public void ExecuteNonQuery()
        {
            if (_connection.State != ConnectionState.Open)
                throw new Exception("Debe existir una conexión abierta para poder ejecutar esta instrucción");

            _command.ExecuteNonQuery();
        }

        #endregion

        #region Funciones
        public DataTable DbExecute()
        {
            var dataTable = new DataTable();
            var adaptador = new SqlDataAdapter(_command);
            adaptador.Fill(dataTable);
            return dataTable;
        }

        public int DbNextId()
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    throw new Exception("Debe existir una conexión abierta para poder ejecutar esta instrucción");

                var id = _command.ExecuteScalar();

                if (id == DBNull.Value)
                    return 1;

                return int.Parse(id.ToString()) + 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion
    }
}
