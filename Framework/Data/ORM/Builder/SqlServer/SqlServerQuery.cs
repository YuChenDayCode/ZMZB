using Framework.Reflection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Framework.Data.ORM
{
    public class SqlServerQuery<T> : Query<T>
    {
        private int _top = -1;
        public override Query<T> Top(int num)
        {
            this._top = num;
            return this;
        }
        public override ISqlDocker Build()
        {
            var sql = new StringBuilder();
            if (_sqlCount != null) sql.Append(_sqlCount.CountSql);
            else
            {
                sql.Append("SELECT");
                sql.Append(this._top > -1 ? $" TOP { this._top }" : string.Empty);
                sql.Append($" {string.Join(",", from t in this.propertyMaps select t.GetQueryField())} FROM {this.entitymap.TabelName}");
            }
            if (_where != null)
            {
                sql.Append($" WHERE {_where.ToString()} ");
            }
            if (_sqlPaging != null)
            {
                this._top = -1;
                sql.Append(_sqlPaging.PagingSql);
            }

            var docker = new SqlDocker() { Sql = sql.ToString(), CommandType = CommandType.Text };
            if (_where != null)
            {
                var dic = new Dictionary<string, object>();
                _where.GetDictionary(dic);
                docker.Parameters = dic;
            }
            return docker;
        }

        public override Query<T> where(Expression<Func<T, object>> expression)
        {
            this._where = Where.Parse(expression, this.propertyMaps);
            return this;
        }

        public override Query<T> Count(Expression<Func<T, object>> field = null)
        {
            string CountKey = field == null ? this.propertyMaps.First(m => m.PrimaryKey != null).Name : ReflectionExtension.GetProperty(field).Name;
            string sql = $"SELECT COUNT({ this.entitymap.TabelName}.{CountKey}) FROM {this.entitymap.TabelName}";
            _sqlCount = new SqlCount(sql);
            return this;

        }

        public override Query<T> Sort(Expression<Func<T, object>> sort_field, string sortWay = " Asc ")
        {
            //SqlSort ss = new SqlSort() { Sort_Field = ReflectionExtension.GetProperty(sort_field).Name, SortWay = sortWay };
            return this;
        }
        public override Query<T> Paging(int pageIndex, int pageSize, Expression<Func<T, object>> sort_field = null, string sortWay = null, Expression<Func<T, object>> sort_field1 = null, string sortWay1 = null)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            var PagingSql = new StringBuilder();
            string sort_str = sort_field == null ? string.Empty : $" ORDER BY {ReflectionExtension.GetProperty(sort_field).Name} {sortWay}";
            PagingSql.Append(sort_str);
            sort_str = sort_field1 == null ? string.Empty : $" ,{ReflectionExtension.GetProperty(sort_field1).Name} {sortWay1}";
            PagingSql.Append(sort_str);

            PagingSql.Append($" LIMIT { (pageIndex - 1) * pageSize},{pageSize}");

            _sqlPaging = new SqlPaging(PagingSql.ToString());
            return this;
        }

    }


}
