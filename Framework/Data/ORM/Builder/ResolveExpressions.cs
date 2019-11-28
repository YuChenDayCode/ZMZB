using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Framework.Data.ORM
{
    public class IndexContent
    {
        public int Index { get; set; } = 1;
    }
    public class ResolveExpressions
    {
        //Where item;
        private PropertyInfo _value;
        public void Rosolve(Expression exp, Where item, IndexContent indexContent, bool IsValue = false)
        {
            if (item == null)
            {
                item = new Where(++indexContent.Index,item.propertymap);
            }
            BinaryExpression bexp;
            switch (exp.NodeType)
            {
                case ExpressionType.Lambda:
                    exp = ((LambdaExpression)exp).Body;
                    this.Rosolve(exp, item, indexContent, IsValue);
                    break;
                case ExpressionType.Convert:
                    exp = ((UnaryExpression)exp).Operand;
                    this.Rosolve(exp, item, indexContent, IsValue);
                    break;
                case ExpressionType.AndAlso:
                    bexp = (BinaryExpression)exp;
                    item.SpliceWay = " and ";
                    item.Left = new Where(++indexContent.Index,item.propertymap);
                    this.Rosolve(bexp.Left, item.Left as Where, indexContent, IsValue);
                    item.Right = new Where(++indexContent.Index, item.propertymap);
                    this.Rosolve(bexp.Right, item.Right as Where, indexContent, IsValue);
                    break;
                case ExpressionType.OrElse:
                    bexp = (BinaryExpression)exp;
                    item.SpliceWay = " or ";
                    item.Left = new Where(++indexContent.Index,item.propertymap);
                    this.Rosolve(bexp.Left, item.Left as Where, indexContent, IsValue);
                    item.Right = new Where(++indexContent.Index,item.propertymap);
                    this.Rosolve(bexp.Right, item.Right as Where, indexContent, IsValue);
                    break;
                case ExpressionType.MemberAccess:
                    MemberExpression((MemberExpression)exp, item,indexContent, IsValue);
                    break;
                case ExpressionType.Call:
                    MethodCallExpression((MethodCallExpression)exp, item,indexContent);
                    break;
                case ExpressionType.Equal:
                    bexp = (BinaryExpression)exp;
                    item.Operator = " = ";
                    this.Rosolve(bexp.Left, item, indexContent, IsValue);
                    this.Rosolve(bexp.Right, item, indexContent, true);
                    break;
                case ExpressionType.NotEqual:
                    bexp = (BinaryExpression)exp;
                    item.Operator = " <> ";
                    this.Rosolve(bexp.Left, item, indexContent, IsValue);
                    this.Rosolve(bexp.Right, item, indexContent, true);
                    break;
                case ExpressionType.LessThan:
                    bexp = (BinaryExpression)exp;
                    item.Operator = " < ";
                    this.Rosolve(bexp.Left, item, indexContent, IsValue);
                    this.Rosolve(bexp.Right, item, indexContent, true);
                    break;
                case ExpressionType.LessThanOrEqual:
                    bexp = (BinaryExpression)exp;
                    item.Operator = " <= ";
                    this.Rosolve(bexp.Left, item, indexContent, IsValue);
                    this.Rosolve(bexp.Right, item, indexContent, true);
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    bexp = (BinaryExpression)exp;
                    item.Operator = " >= ";
                    this.Rosolve(bexp.Left, item, indexContent, IsValue);
                    this.Rosolve(bexp.Right, item, indexContent, true);
                    break;
                case ExpressionType.GreaterThan:
                    bexp = (BinaryExpression)exp;
                    item.Operator = " > ";
                    this.Rosolve(bexp.Left, item, indexContent, IsValue);
                    this.Rosolve(bexp.Right, item, indexContent, true);
                    break;
                case ExpressionType.Constant:
                    this.GetConstantValue((ConstantExpression)exp, item);
                    break;
            }
        }

        private void MethodCallExpression(MethodCallExpression expr, Where node,IndexContent indexContent)
        {

            switch (expr.Method.Name)
            {
                case nameof(string.Contains):
                    node.Operator = " like ";
                    break;
            }
            this.Rosolve(expr.Object, node,indexContent);
            if (expr.Arguments.Count() > 0)
            {
                this.Rosolve(expr.Arguments.First(), node, indexContent, true);
            }
        }
        private void MemberExpression(MemberExpression expr, Where node,IndexContent indexContent, bool isValue = false)
        {

            if (node != null)
            {
                switch (expr.Member.MemberType)
                {
                    case MemberTypes.Property:
                        this.PropertyExpression(expr, node, indexContent, isValue);
                        break;
                    case MemberTypes.Field:
                        this.FieldExpression(expr, node);
                        break;
                }
            }
        }
        private void PropertyExpression(MemberExpression expr, Where item, IndexContent indexContent, bool IsValue)
        {
            if (IsValue)
            {
                this._value = expr.Member as PropertyInfo;
                this.Rosolve(expr.Expression, item, indexContent, IsValue);
            }
            else
            {
                var p = item.propertymap.First(t => t.PropertyInfo == expr.Member as PropertyInfo);
                item.property = p;
            }
        }
        private void FieldExpression(MemberExpression mexpr, Where item)
        {
            var expr = mexpr.Expression;
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
                    case ExpressionType.Constant:
                        this.GetConstantValue((ConstantExpression)expr, item, mexpr.Member.Name);
                        return;
                    default:
                        expr = ((MemberExpression)expr).Expression;
                        break;
                }
            }
        }
        private void GetConstantValue(ConstantExpression expr, Where item, string fieldName = null)
        {

            if (item != null)
            {
                if (fieldName == null)
                {
                    item.Value = expr.Value;
                }
                else
                {
                    var member = expr.Value.GetType().GetField(fieldName);
                    if (this._value != null)
                    {
                        item.Value = _value.GetValue(member.GetValue(expr.Value), null);
                        this._value = null;
                    }
                    else
                    {
                        if (member != null)
                        {
                            if (member.FieldType.IsEnum)
                            {
                                item.Value = (int)member.GetValue(expr.Value);
                            }
                            else
                            {
                                item.Value = member.GetValue(expr.Value);
                            }
                        }
                        else
                        {
                            item.Value = expr.Value;
                        }

                    }
                }
            }
        }
    }
}
