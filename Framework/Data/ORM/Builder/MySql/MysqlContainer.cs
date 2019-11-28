using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Framework.Data.ORM
{
    public class MysqlContainer : DBCommond
    {
       
        public MysqlContainer() : base()
        {
        }

        protected override IDbConnect GetIDbConntion()
        {
            return new MySqlDbConnect();
        }

        protected override void SetParameter(IDataParameterCollection mysqlpara, IEnumerable<KeyValuePair<string, object>> Parameters)
        {
            if (Parameters == null) return;
            foreach (var para in Parameters)
            {
                mysqlpara.Add(new MySqlParameter(para.Key, para.Value));
            }

        }
    }
}
