
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Framework.Data.ORM
{
    public class MySqlDbConnect : IDbConnect
    {
        private MySqlConnection _con;
        public IDbConnection GetIDbConnection()
        {
            this._con = this._con ?? new MySqlConnection(MySqlDbConnectionConfig.GetSqlDbConntionConfigure());
            return this._con;
        }

        public IDbCommand GetIDbCommand()
        {
            MySqlCommand mySqlCommand = null;
            if (this._con == null)
            {
                this._con = this._con ?? new MySqlConnection(MySqlDbConnectionConfig.GetSqlDbConntionConfigure());
            }
            if (this._con.State == ConnectionState.Closed)
            {
                this._con.Open();
            }
            mySqlCommand = this._con.CreateCommand();
            return mySqlCommand;
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
