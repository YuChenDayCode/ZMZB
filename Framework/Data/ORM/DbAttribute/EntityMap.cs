using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Data.ORM
{
    /// <summary>
    /// 映射数据库表名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityMapper_TableName : Attribute
    {
        readonly string _tablename;

        public string AliasName => _tablename;

        /// <summary>
        /// 映射数据库表名
        /// </summary>
        /// <param name="tablename"></param>
        public EntityMapper_TableName(string table_name)
        {
            _tablename = table_name;
        }
    }

    /// <summary>
    /// 映射数据库列名
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EntityMapper_ColumnName : Attribute
    {
        readonly string _columnName;

        public string AliasName => _columnName;

        /// <summary>
        /// 映射列名
        /// </summary>
        /// <param name="column_name"></param>
        public EntityMapper_ColumnName(string column_name)
        {
            _columnName = column_name;
        }
    }

    /// <summary>
    /// 标识不属于数据库字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class EntityMapper_Ignore : Attribute
    {
        public string Ignore { get; }
    }
}
