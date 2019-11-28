using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Framework.Data.ORM
{
    public class SqlServerContainer : DBCommond
    {
       
        public SqlServerContainer() : base()
        {
        }

        protected override IDbConnect GetIDbConntion()
        {
            return new SqlServerDbConnect();
        }

        protected override void SetParameter(IDataParameterCollection mysqlpara, IEnumerable<KeyValuePair<string, object>> Parameters)
        {
            if (Parameters == null) return;
            foreach (var para in Parameters)
            {
                mysqlpara.Add(new SqlParameter(para.Key, para.Value));
            }

        }
    }
}
