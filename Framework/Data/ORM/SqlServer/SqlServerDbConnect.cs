
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Framework.Data.ORM
{
    public class SqlServerDbConnect : IDbConnect
    {
        private SqlConnection _con;
        public IDbConnection GetIDbConnection()
        {
            this._con = this._con ?? new SqlConnection(SqlServerDbConnectConfig.GetSqlDbConntionConfigure());
            return this._con;
        }

        public IDbCommand GetIDbCommand()
        {
            SqlCommand sqlCommand = null;
            if (this._con == null)
            {
                this._con = this._con ?? new SqlConnection(SqlServerDbConnectConfig.GetSqlDbConntionConfigure());
            }
            if (this._con.State == ConnectionState.Closed)
            {
                this._con.Open();
            }
            sqlCommand = this._con.CreateCommand();
            return sqlCommand;
        }

        public void Close()
        {
            if (this._con != null)
            {
                this._con.Close();
            }
        }
    }
}
