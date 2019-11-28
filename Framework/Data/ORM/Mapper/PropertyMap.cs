using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Framework.Data.ORM
{
    public class PropertyMap<T> : IPropertyMap
    {
        public PropertyInfo PropertyInfo { get; }
        public Type DataType => PropertyInfo.PropertyType;

        public bool Ignore { get; }

        public string ColumnName { get; }

        public PrimaryKey PrimaryKey { get; }

        public string Name => PropertyInfo.Name;


        public string TableName { get; }

        public PropertyMap(PropertyInfo propertyInfo, string tableName)
        {
            this.PropertyInfo = propertyInfo;
            this.TableName = tableName;
            this.Ignore = GetIgnoreAttribute(propertyInfo);
            this.PrimaryKey = propertyInfo.GetCustomAttribute<PrimaryKey>();
            this.ColumnName = GetColumnAttribute(propertyInfo);
        }

        static bool GetIgnoreAttribute(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<EntityMapper_Ignore>() != null;
        }
        static string GetColumnAttribute(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<EntityMapper_ColumnName>()?.AliasName ?? propertyInfo.Name;
        }
        static PrimaryKey GetPrimaryKeyAttribute(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttribute<PrimaryKey>();
        }

        public string GetParamName(string paramMark = "@")
        {
            return $"{paramMark}{GetMapperColumnName("_")}";
        }

        public string GetMapperColumnName(string split = ".")
        {
            return $"{this.TableName}{split}{this.ColumnName}";
        }

        public string GetQueryField()
        {
            return $"{this.GetMapperColumnName()} as {this.GetMapperColumnName("_")}";
        }
    }
}
