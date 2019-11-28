using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Framework.Data.ORM
{
    public class Where : ResolveExpressions
    {
        readonly int _Index;
        //public int Index => _Index;
        public int Index { get { return _Index; } }
        readonly string __where;
        public string _where => __where;

        public Where Left { get; set; }
        public Where Right { get; set; }
        public string Operator { get; set; }
        public string SpliceWay { get; set; }

        public IEnumerable<IPropertyMap> propertymap;
        public string Name => property?.ColumnName;
        public IPropertyMap property { get; set; }

        public dynamic Value { get; set; }
        public Where(string where)
        {
            __where = where;
        }
        public Where(int index, IEnumerable<IPropertyMap> _propertymap)
        {
            _Index = index;
            propertymap = _propertymap;
        }

        public static Where Parse(LambdaExpression expressions, IEnumerable<IPropertyMap> propertymap)
        {
            var indexcontent = new IndexContent();
            var re = new Where(indexcontent.Index, propertymap);
            re.Rosolve(expressions, re, indexcontent);
            return re;
        }
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(this._where))
            {
                return this._where;
            }
            else if (Left == null || Right == null)
            {
                return $" {property.TableName}.{Name} { Operator } { property.GetParamName()}{Index}";
            }
            else
            {
                return $" ({Left.ToString()} { this.SpliceWay.ToString()} {Right.ToString()})";
            }
        }

        public void GetDictionary(Dictionary<string, object> dic)
        {
            if (!string.IsNullOrEmpty(this._where))
            {
                return;
            }
            else if (Left == null || Right == null)
            {
                dic.Add($"{this.property.GetParamName()}{Index}", this.Value);
            }
            else
            {
                if (Left != null)
                {
                    Left.GetDictionary(dic);
                }
                if (Right != null)
                {
                    Right.GetDictionary(dic);
                }
            }
        }
    }
}
