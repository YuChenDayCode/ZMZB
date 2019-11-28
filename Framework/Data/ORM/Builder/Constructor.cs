using Framework.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Framework.Data.ORM
{
    public enum DMLType
    {
        INSERT = 1,
        UPDATE = 2,
        DELETE = 3
    }

    /// <summary>
    /// 构造增删改
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Constructor<T>
    {
        readonly EntityMapper<T> entitymap;
        private DMLType _DMLType;
        protected T entity;
        protected IEnumerable<IPropertyMap> property;
        private Func<IEnumerable<IPropertyMap>, string> format;
        protected Where _where;
        public Constructor()
        {
            this.entitymap = new EntityMapper<T>();
        }
        public Constructor<T> Insert(T entity)
        {
            _DMLType = DMLType.INSERT;
            this.entity = entity;
            this.property = from t in this.entitymap.PropertyMaps
                            where t.PrimaryKey == null && t.Ignore == false
                            select t;
            this.format = new Func<IEnumerable<IPropertyMap>, string>(BuildInsert);
            return this;
        }
        public Constructor<T> Insert_Return_Id(T entity)
        {
            _DMLType = DMLType.INSERT;
            this.entity = entity;
            this.property = from t in this.entitymap.PropertyMaps
                            where t.PrimaryKey == null && t.Ignore == false
                            select t;
            this.format = new Func<IEnumerable<IPropertyMap>, string>(BuildInsert_Return_Id);
            return this;
        }
        public Constructor<T> Update(T entity)
        {
            _DMLType = DMLType.UPDATE;
            this.entity = entity;
            this.property = from t in this.entitymap.PropertyMaps
                            where t.PrimaryKey == null && t.Ignore == false
                            select t;
            this.format = new Func<IEnumerable<IPropertyMap>, string>(BuildUpdate);
            return this;
        }
        public Constructor<T> Update(T entity, params string[] arrs)
        {
            _DMLType = DMLType.UPDATE;
            var partmap = from t in this.entitymap.PropertyMaps where arrs.Contains(t.Name) select t;
            this.entity = entity;
            this.property = from t in partmap
                            where t.PrimaryKey == null && t.Ignore == false
                            select t;
            this.format = new Func<IEnumerable<IPropertyMap>, string>(BuildUpdate);
            return this;
        }
        public Constructor<T> Delete()
        {
            _DMLType = DMLType.DELETE;
            this.format = null;
            return this;
        }
        public Constructor<T> Delete<K>(IEnumerable<K> pkvlues)
        {
            _DMLType = DMLType.DELETE;
            string pkName = this.entitymap.PropertyMaps.First(m => m.PrimaryKey != null)?.Name;
            if (typeof(K).Name == typeof(string).Name)
            {
                var pk = from t in pkvlues select $"'{t}'";
                _where = new Where($"{pkName} in ({string.Join(",", pk)})");
            }
            if (typeof(K).Name == typeof(int).Name)
                _where = new Where($"{pkName} in ({string.Join(",", pkvlues)})");

            this.format = null;
            return this;
        }

        public Constructor<T> where(Expression<Func<T, object>> expressions)
        {
            _where = Where.Parse(expressions, this.entitymap.PropertyMaps);
            return this;
        }
        public Constructor<T> where(string where)
        {
            _where = new Where(where);
            return this;
        }

        /// <summary>
        /// 构造语句
        /// </summary>
        /// <returns></returns>
        public ISqlDocker Build()
        {
            var sql = new StringBuilder();
            sql.Append(this._DMLType.ToString());
            sql.Append(this._DMLType == DMLType.DELETE ? " FROM " : string.Empty);
            sql.Append(this.entitymap.TabelName.Fill());
            sql.Append(this.format == null ? string.Empty : format(this.property));
            if (_where != null)
            {
                sql.Append($" WHERE {_where.ToString()}");
            }
            return new SqlDocker() { Sql = sql.ToString(), CommandType = CommandType.Text, Parameters = this.GetParameter() };
        }

        public ISqlDocker CreateCustomerSql(string sql, CommandType commandType = CommandType.Text, IEnumerable<KeyValuePair<string, object>> para = null)
        {
            return new SqlDocker() { Sql = sql, CommandType = commandType, Parameters = para };
        }


        protected abstract string BuildInsert(IEnumerable<IPropertyMap> propertys);
        protected abstract string BuildInsert_Return_Id(IEnumerable<IPropertyMap> propertys);

        protected abstract string BuildUpdate(IEnumerable<IPropertyMap> propertys);

        protected abstract IEnumerable<KeyValuePair<string, object>> GetParameter();
    }
}
