using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Framework.Data.ORM.Enum;

namespace Framework.Data.ORM
{
    public abstract class Query<T>
    {
        protected readonly EntityMapper<T> entitymap;
        protected IEnumerable<IPropertyMap> propertyMaps;
        protected Where _where;
        protected SqlCount _sqlCount;
        protected SqlPaging _sqlPaging;

        public abstract Query<T> Top(int num);
        public abstract Query<T> where(Expression<Func<T, object>> expression);
        public abstract Query<T> Count(Expression<Func<T, object>> expression = null);
        public abstract Query<T> Sort(Expression<Func<T, object>> sort_field, string sortWay);

        public abstract Query<T> Paging(int pageIndex, int pageSize, Expression<Func<T, object>> sort_field = null, string sortWay = null, Expression<Func<T, object>> sort_field1 = null, string sortWay1 = null);
        public abstract ISqlDocker Build();

        public Query()
        {
            this.entitymap = new EntityMapper<T>();
            this.propertyMaps = from t in entitymap.PropertyMaps where t.Ignore == false select t;
        }
    }

    public class SqlCount
    {
        readonly string _CountSql;
        public string CountSql => _CountSql;
        public SqlCount(string _conuntSql)
        {
            _CountSql = _conuntSql;
        }
    }

    public class SqlPaging
    {
        readonly string _Sql;
        public string PagingSql => _Sql;
        public SqlPaging(string _pagingSql)
        {
            _Sql = _pagingSql;
        }
    }
}
