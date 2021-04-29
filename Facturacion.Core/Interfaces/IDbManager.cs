using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Facturacion.Core.Interfaces
{
    public interface IDbManager
    {
        void Connect();
        void Disconnect();
        void TransactionBegin();
        void TransactionBegin(IsolationLevel isolationLevel);
        void TransactionCommit();
        void TransactionRollback();
        void CreateCommand(string sql, List<SqlParameter> parameters, CommandType type);
        void ExecuteNonQuery();
        DataTable DbExecute();
        int DbNextId();
    }
}
