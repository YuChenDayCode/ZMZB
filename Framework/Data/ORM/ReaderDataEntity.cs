using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.Data.ORM
{
    class ReaderDataEntity<T> where T : new()
    {
        readonly IDataReader reader;
        readonly Dictionary<int, string> ReaderColumnNames;
        readonly Dictionary<int, IPropertyMap> MapperColumnNames;
        public ReaderDataEntity(IDataReader reader)
        {
            var entitymap = new EntityMapper<T>();
            this.reader = reader;
            this.ReaderColumnNames = GetReaderColumnName(reader);
            this.MapperColumnNames = (from t1 in this.ReaderColumnNames
                                      select new KeyValuePair<int, IPropertyMap>(
                                          t1.Key,
                                          (from t in entitymap.PropertyMaps where t.GetMapperColumnName("_") == t1.Value || t.Name == t1.Value select t).FirstOrDefault()
                                      )).ToDictionary(k => k.Key, v => v.Value);
        }

        public Dictionary<int, string> GetReaderColumnName(IDataReader reader)
        {
            var dic = new Dictionary<int, string>(reader.FieldCount);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                dic.Add(i, reader.GetName(i));
            }
            return dic;
        }

        public IEnumerable<T> ConvertDataEntity()
        {
            var list = new List<T>();
            while (reader.Read())
            {
                T entity = new T();
                for (int i = 0; i < MapperColumnNames.Count; i++)
                {
                    if (MapperColumnNames[i] == null) { entity = default;continue; }

                    var v = ConvertValue(MapperColumnNames[i], reader[i]);
                    PropertyInfo pi = typeof(T).GetProperty(MapperColumnNames[i].Name);
                    if (pi != null)
                        pi.SetValue(entity, v);

                }
                list.Add(entity);
            }
            return list;
        }


        object ConvertValue(IPropertyMap propertyMap, object value)
        {
            if (Convert.IsDBNull(value) || value == null)
                return default(T);
            if (propertyMap.DataType == typeof(bool))
            {
                bool tempbool = false;
                if (bool.TryParse(value.ToString(), out tempbool)) return tempbool;
                if (string.IsNullOrEmpty(value.ToString()) || value.ToString() == "0") return false;
                return true;
            }
            if (propertyMap.DataType == typeof(DateTime))
            {
            }

            return value;

        }
        public static object ConvertValue(Type type, object value)
        {
            if (Convert.IsDBNull(value) || (value == null))
            {
                return null;
            }

            Type type2 = value.GetType();
            if (type == type2)
            {
                return value;
            }
            if (((type == typeof(Guid)) || (type == typeof(Guid?))) && (type2 == typeof(string)))
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    return null;
                }
                return new Guid(value.ToString());
            }
            if (((type == typeof(DateTime)) || (type == typeof(DateTime?))) && (type2 == typeof(string)))
            {
                if (string.IsNullOrEmpty(value.ToString()))
                {
                    return null;
                }
                return Convert.ToDateTime(value);
            }
            //if (type.IsEnum)
            //{
            //    try
            //    {
            //        return Enum.Parse(type, value.ToString(), true);
            //    }
            //    catch
            //    {
            //        return Enum.ToObject(type, value);
            //    }
            //}
            if (((type == typeof(bool)) || (type == typeof(bool?))))
            {
                bool tempbool = false;
                if (bool.TryParse(value.ToString(), out tempbool))
                {
                    return tempbool;
                }
                else
                {
                    if (string.IsNullOrEmpty(value.ToString()))
                        return false;
                    else
                    {
                        if (value.ToString() == "0")
                        {
                            return false;
                        }
                        return true;
                    }
                }

            }

            if (type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }

            return Convert.ChangeType(value, type);
        }

    }
}
