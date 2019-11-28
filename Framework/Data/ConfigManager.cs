using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Framework.Data
{
    public class ConfigManager<T> where T : class, new()
    {
        internal static ConcurrentDictionary<string, T> config_dic;
        static ConfigManager()
        {
            config_dic = new ConcurrentDictionary<string, T>(); //线程安全
        }
        //索引函数
        public T this[string key]
        {
            get
            {
                var t = default(T);
                config_dic.TryGetValue(key, out t);
                return t;
            }
        }
        public XDocument LocalConfig(string addres)
        {
            //判断是否是虚拟路径,如果不是则转换为绝对路径
            string str = Path.IsPathRooted(addres) ? addres : System.IO.Path.GetFullPath(addres);
            if (!File.Exists(@addres))
                throw new Exception("配置文件不存在！请检查生成的文件是否包含");
            return XDocument.Load(str);
        }

        protected static T CreaterConfigureItem(XElement element)
        {
            var config = new T();
            var ps = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            for (var i = 0; i < ps.Length; i++)
            {
                var a = element.Element(ps[i].Name);
                if (a != null)
                {
                    ps[i].SetValue(config, Convert.ChangeType(a.Value, ps[i].PropertyType));
                }
            }
            return config;
        }
    }
}
