using Framework.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Framework.Extension
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 转换SQL 匹配类型枚举
        /// </summary>
        /// <param name="compare">匹配类型对象</param>
        /// <returns></returns>

        /// <summary>
        /// 读取枚举类型描述信息
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="item">枚举对象</param>
        /// <returns></returns>
        public static string GetDescribeInfo<T>(this T item)
        {
            Type type = item.GetType();//枚举无法匹配的情况
            FieldInfo aa = type.GetField(item.ToString(), BindingFlags.Public | BindingFlags.Static);
            if (aa == null) return "";

            var a = ReflectionExtension.GetFieldInfoAttribute<DescriptionAttribute>(item);
            return a == null ? string.Empty : a.Description;
        }
        /// <summary>
        /// 读取枚举类型描述信息
        /// </summary>
        /// <typeparam name="T">要创建的枚举类型</typeparam>
        /// <param name="item">枚举值</param>
        /// <returns></returns>
        public static string GetDescribeInfo<T>(this int item)
        {
            var t = typeof(T);
            if (t.IsEnum)
            {
                var a = (T)System.Enum.Parse(t, item.ToString());
                return EnumExtensions.GetDescribeInfo(a);
            }
            else
            {
                return string.Empty;
            }

        }



        /// <summary>
        /// 根据枚举text获取描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetDescribeInfo<T>(this string str)
        {
            try
            {
                T en = (T)Enum.Parse(typeof(T), str);
                return en.GetDescribeInfo();
            }
            catch (Exception ex)
            {
                return string.Empty;
                //throw new ArgumentException("未找到对应枚举值");
            }
        }





        /// <summary>
        /// 根据值获得枚举中选中行
        /// </summary>
        /// <typeparam name="T">枚举泛型</typeparam>
        /// <param name="value">枚举值</param>
        /// <returns></returns>
        public static IEnumerable<SelectListItem> GetEnumListItem<T>(this int value) where T : struct
        {
            IEnumerable<SelectListItem> list = null;
            var dic = ReflectionExtension.GetEnumDescribeInfos<T>();
            if (value > 0)
            {
                list = from t in dic select new SelectListItem { Value = t.Key.ToString(), Text = t.Value, Selected = t.Key == value };
            }
            else
            {
                list = from t in dic select new SelectListItem { Value = t.Key.ToString(), Text = t.Value };
            }
            return list;
        }

        /// <summary>
        /// 创建枚举
        /// </summary>
        /// <typeparam name="K">要创建的枚举类型</typeparam>
        /// <param name="item">枚举值</param>
        /// <returns></returns>
        public static K GetEnum<K>(this int item)
        {
            var t = typeof(K);
            if (t.IsEnum)
            {
                return (K)System.Enum.Parse(t, item.ToString());

            }
            else
            {
                return default(K);
            }

        }

        /// <summary>
        /// 获取该枚举的所有描述的描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<T, string> GetEnumDescriptionList<T>()
        {
            var description = new Dictionary<T, string>();
            var fis = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
            if (fis != null && fis.Length > 0)
            {
                foreach (var item in fis)
                {
                    var attributes =
                   (DescriptionAttribute[])item.GetCustomAttributes(
                   typeof(DescriptionAttribute),
                   false);
                    var value = item.GetValue(null);
                    if (attributes.Length > 0)
                        description.Add((T)value, attributes[0].Description);
                }
            }
            return description;
        }

        /// <summary>
        /// 获取枚举数据列表
        /// </summary>
        /// <returns></returns>
        public static List<EnumEntity> GetEnumLists<T>()
        {

            List<EnumEntity> list = new List<EnumEntity>();
            foreach (var d in Enum.GetValues(typeof(T)))
            {
                EnumEntity e = new EnumEntity();
                e.EnumValue = (int)d;
                e.EnumName = d.ToString();
                e.Desction = d.GetDescribeInfo();
                list.Add(e);
            }
            return list;
        }

     
        /// <summary>
        /// 读取枚举类型Keytext
        /// </summary>
        /// <typeparam name="T">要创建的枚举类型</typeparam>
        /// <param name="item">枚举值</param>
        /// <returns></returns>
        public static string GetKey<T>(this int item)
        {
            var t = typeof(T);
            if (t.IsEnum)
            {
                var a = (T)System.Enum.Parse(t, item.ToString());
                return a.ToString();
            }
            else
            {
                return string.Empty;
            }

        }


    }

    public class EnumEntity
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Desction { get; set; }
        /// <summary>
        /// key名称
        /// </summary>
        public string EnumName { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public int EnumValue { get; set; }
    }
}
