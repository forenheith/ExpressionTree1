using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;

namespace ExpressionTree1
{
    public class ExpVisitor : ExpressionVisitor
    {
        //protected override Expression VisitBinary(BinaryExpression node)
        //{
        //    if (node.Equals(null))
        //        return base.VisitBinary(node);

        //    if (!node.NodeType.Equals(ExpressionType.Add) && !node.NodeType.Equals(ExpressionType.Subtract))
        //        return base.VisitBinary(node);

        //    ConstantExpression constant = null;
        //    if (node.Left.NodeType.Equals(ExpressionType.Constant) ||
        //        node.Right.NodeType.Equals(ExpressionType.Constant))
        //    {
        //        constant = node.Left.NodeType.Equals(ExpressionType.Constant)
        //            ? (ConstantExpression) node.Left
        //            : (ConstantExpression) node.Right;
        //    }

        //    ParameterExpression parameter = null;
        //    if (node.Left.NodeType.Equals(ExpressionType.Parameter) ||
        //        node.Right.NodeType.Equals(ExpressionType.Parameter))
        //    {
        //        parameter = node.Left.NodeType.Equals(ExpressionType.Parameter)
        //            ? (ParameterExpression) node.Left
        //            : (ParameterExpression) node.Right;
        //    }

        //    if (!NeedToTransform(parameter, constant))
        //        return base.VisitBinary(node);

        //    return node.NodeType == ExpressionType.Add ? Expression.Increment(node) : Expression.Decrement(node);
        //}

        private static bool NeedToTransform(ParameterExpression parameter, ConstantExpression constant)
        {
            if (parameter != null && constant != null)
            {
                return (parameter.Name.Equals("1") || constant.Value.Equals(1));
            }

            return parameter?.Name.Equals("1") ?? (constant?.Value.Equals(1) ?? false);
        }

        public Expression VLambda()
        {
            Expression<Func<int, int>> exp = (a) => a + 5;
            return VisitLambda(exp);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            //var exp = Visit(node);
            return Expression.Lambda(Visit(node.Body));
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return Expression.Constant(node);
        }
    }
}