using Framework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Framework.Reflection
{
    public static class ReflectionExtension
    {
        public static PropertyInfo GetProperty(LambdaExpression lambda)
        {
            Expression expr = lambda;
            for (; ; )
            {
                switch (expr.NodeType)
                {
                    case ExpressionType.Lambda:
                        expr = ((LambdaExpression)expr).Body;
                        break;
                    case ExpressionType.Convert:
                        expr = ((UnaryExpression)expr).Operand;
                        break;
                    case ExpressionType.MemberAccess:
                        MemberExpression memberExpression = (MemberExpression)expr;
                        MemberInfo mi = memberExpression.Member;
                        return mi as PropertyInfo;
                    default:
                        return null;
                }
            }
        }
        public static T GetClassAttribute<T>(dynamic obj) where T : Attribute, new()
        {
            Type type = obj.GetType();
            object[] records = type.GetCustomAttributes(typeof(T), true);
            return records.Length > 0 ? (records[0] as T) : default(T);
        }
        public static T GetClassAttribute<T>(Type type) where T : Attribute, new()
        {

            object[] records = type.GetCustomAttributes(typeof(T), true);
            return records.Length > 0 ? (records[0] as T) : default(T);
        }

        public static T GetFieldInfoAttribute<T>(dynamic obj) where T : Attribute, new()
        {
            Type type = obj.GetType();
            FieldInfo a = type.GetField(obj.ToString(), BindingFlags.Public | BindingFlags.Static);
            var query = from q in a.GetCustomAttributes(typeof(T), false) select ((T)q);
            return query.FirstOrDefault();
        }
        public static IEnumerable<KeyValuePair<int, string>> GetEnumDescribeInfos<T>() where T : struct
        {
            var a = typeof(T);
            var dic = new Dictionary<int, string>();
            if (a.IsEnum)
            {
                foreach (var b in Enum.GetValues(a))
                {

                    var d = EnumExtensions.GetDescribeInfo((T)Enum.Parse(a, b.ToString()));
                    dic.Add((int)b, d);
                }
            }
            return dic;

        }
    }
}
