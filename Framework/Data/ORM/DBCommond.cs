using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Framework.Data.ORM
{
    public abstract class DBCommond
    {
        readonly IDbConnect conn;
        protected IDbConnect Connect => conn;
        public DBCommond()
        {
            this.conn = this.GetIDbConntion();

        }
        protected abstract IDbConnect GetIDbConntion();
        protected abstract void SetParameter(IDataParameterCollection mysqlpara, IEnumerable<KeyValuePair<string, object>> Parameters);

        private IDbCommand CreateCommond(ISqlDocker container)
        {
            var cmd = this.conn.GetIDbCommand();
            cmd.CommandText = container.Sql;
            cmd.CommandType = container.CommandType;
            this.SetParameter(cmd.Parameters, container.Parameters);
            return cmd;
        }



        public void ExecuteNonQuery(ISqlDocker sqlDocker, out int row)
        {
            IDbCommand cmd = CreateCommond(sqlDocker);
            try
            {
                row = cmd.ExecuteNonQuery();
            }
            //catch (Exception ex) { }
            finally
            {
                CloseConn(this.conn);
            }
        }

        public IEnumerable<T> ExecuteReader<T>(ISqlDocker sqlDocker) where T : new()
        {

            T entity = new T();
            var list = new List<T>();

            var con = GetIDbConntion();
            var cmd = CreateCommond(sqlDocker);
            using (var read = cmd.ExecuteReader())
            {
                var rde = new ReaderDataEntity<T>(read);
                return rde.ConvertDataEntity();
            }
        }
        public void ExecuteScalar<T>(ISqlDocker sqlDocker, out T value)
        {
            value = default(T);
            var con = GetIDbConntion();
            var cmd = CreateCommond(sqlDocker);
            using (var read = cmd.ExecuteReader())
            {
                while (!read.IsClosed && read.Read())
                {
                    value = (T)Convert.ChangeType(read.GetValue(0), typeof(T));
                }
            }
        }

        private void CloseConn(IDbConnect con)
        {
            con.Close();
        }

    }
}
