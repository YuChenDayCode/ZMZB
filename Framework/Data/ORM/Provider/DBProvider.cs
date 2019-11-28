using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Data.ORM
{
    public abstract class DBProvider<T>
    {
        readonly IDbConnect conn;
        protected IDbConnect IDbCon => conn;
        protected readonly DBCommond dbExecute;

        public DBProvider()
        {
            this.conn = this.GetIDbConntion();
            this.dbExecute = this.CreateExecute();
        }
        protected abstract IDbConnect GetIDbConntion();
        protected abstract DBCommond CreateExecute();
        protected abstract Constructor<T> CreateContructor();
        protected abstract Query<T> CreateQuery();
    }
}
