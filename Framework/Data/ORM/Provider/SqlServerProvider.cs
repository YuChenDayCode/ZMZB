using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace Framework.Data.ORM
{
    public class SqlServerProvider<T> : DBProvider<T>, IDbProvider<T> where T : new()
    {

        public SqlServerProvider() : base() { }

        #region Contructor
        protected override Constructor<T> CreateContructor()
        {
            return new SqlServerConstructor<T>();
        }

        protected override DBCommond CreateExecute()
        {
            return new SqlServerContainer();
        }

        protected override Query<T> CreateQuery()
        {
            return new SqlServerQuery<T>();
        }

        protected override IDbConnect GetIDbConntion()
        {
            return new SqlServerDbConnect();
        }

        #endregion

        public IEnumerable<T> All()
        {
            var q = this.CreateQuery().Build();
            return this.dbExecute.ExecuteReader<T>(q);
        }

        public T Get(Expression<Func<T, object>> exp)
        {
            var q = this.CreateQuery().where(exp).Build();
            return this.dbExecute.ExecuteReader<T>(q).FirstOrDefault();
        }
        public IEnumerable<T> GetList(Expression<Func<T, object>> exp, int top)
        {
            ISqlDocker q;
            if (exp == null && top > -1)
                q = this.CreateQuery().Top(top).Build();
            else
                q = this.CreateQuery().where(exp).Top(top).Build();
            return this.dbExecute.ExecuteReader<T>(q);
        }
        public IEnumerable<T> GetListPage(Expression<Func<T, object>> exp, int PageIndex, int PageSize, out int TotalCount, Expression<Func<T, object>> SortField, string SortWay, Expression<Func<T, object>> SortField1, string SortWay1)
        {
            var c = this.CreateQuery().Count().where(exp).Build();
            this.dbExecute.ExecuteScalar(c, out TotalCount);
            var q = this.CreateQuery().where(exp).Paging(PageIndex, PageSize, SortField, SortWay, SortField1, SortWay1).Build();
            return this.dbExecute.ExecuteReader<T>(q);
        }

        public int Count(Expression<Func<T, object>> exp, Expression<Func<T, object>> CountField)
        {
            int count;
            var q = this.CreateQuery().Count(CountField).where(exp).Build();
            this.dbExecute.ExecuteScalar(q, out count);
            return count;
        }


        public bool Insert(T entity)
        {
            int row;
            var docker = this.CreateContructor().Insert(entity).Build();
            this.dbExecute.ExecuteNonQuery(docker, out row);
            return row > 0;
        }
        public void Insert_Return_Id(T entity, out int Id)
        {
            var docker = this.CreateContructor().Insert_Return_Id(entity).Build();
            this.dbExecute.ExecuteScalar<int>(docker, out Id);
        }

        public bool Update(T entity, Expression<Func<T, object>> expressions)
        {
            int row;
            var d = this.CreateContructor().Update(entity).where(expressions).Build();
            this.dbExecute.ExecuteNonQuery(d, out row);
            return row > 0;
        }

        public bool Update(T entity, Expression<Func<T, object>> expressions, string[] fields)
        {
            int row;
            var d = this.CreateContructor().Update(entity, fields).where(expressions).Build();
            this.dbExecute.ExecuteNonQuery(d, out row);
            return row > 0;
        }

        public int Delete(Expression<Func<T, object>> expressions)
        {
            int row;
            var d = this.CreateContructor().Delete().where(expressions).Build();
            this.dbExecute.ExecuteNonQuery(d, out row);
            return row;
        }

        public int Delete<K>(IEnumerable<K> pkvlues)
        {
            if (pkvlues.Count() <= 0) throw new Exception("无数据");

            int row;
            var d = this.CreateContructor().Delete(pkvlues).Build();
            this.dbExecute.ExecuteNonQuery(d, out row);

            return row;
        }


    }
}
