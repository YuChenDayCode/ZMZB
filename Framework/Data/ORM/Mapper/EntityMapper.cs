using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.Data.ORM
{
    public class EntityMapper<T>
    {
        public string TabelName { get; set; }
        public IEnumerable<IPropertyMap> PropertyMaps { get; }

        public EntityMapper()
        {
            this.PropertyMaps = this.GetPropertyMaps();
        }

        public IEnumerable<IPropertyMap> GetPropertyMaps()
        {
            Type _class = typeof(T);
            this.TabelName = _class.GetCustomAttribute<EntityMapper_TableName>(true)?.AliasName ?? _class?.Name;

            return from t in typeof(T).GetProperties()
                   select CreaterProperty(t, TabelName);
        }

        public static IPropertyMap CreaterProperty(PropertyInfo propertyInfo, string tablename)
        {
            IPropertyMap property;

            property = new PropertyMap<T>(propertyInfo, tablename);
            return property;
        }
       
    }
}
