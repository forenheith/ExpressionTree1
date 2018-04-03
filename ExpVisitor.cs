using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTree1
{
    public class ExpVisitor : ExpressionVisitor
    {
        private readonly Dictionary<string, object> values =
            new Dictionary<string, object> {{"a", 12}, {"b", 7}};

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.Equals(null))
                return base.VisitBinary(node);

            var isAddOrSub = !node.NodeType.Equals(ExpressionType.Add) &&
                             !node.NodeType.Equals(ExpressionType.Subtract);

            ConstantExpression constant = null;
            if (isAddOrSub)
            {
                if (node.Left.NodeType.Equals(ExpressionType.Constant) ||
                    node.Right.NodeType.Equals(ExpressionType.Constant))
                    constant = node.Left.NodeType.Equals(ExpressionType.Constant)
                        ? (ConstantExpression) node.Left
                        : (ConstantExpression) node.Right;

                ParameterExpression parameter = null;
                if (node.Left.NodeType.Equals(ExpressionType.Parameter) ||
                    node.Right.NodeType.Equals(ExpressionType.Parameter))
                    parameter = node.Left.NodeType.Equals(ExpressionType.Parameter)
                        ? (ParameterExpression) node.Left
                        : (ParameterExpression) node.Right;

                if (!NeedToTransform(parameter, constant))
                    return base.VisitBinary(node);

                return node.NodeType == ExpressionType.Add ? Expression.Increment(node) : Expression.Decrement(node);
            }

            return base.VisitBinary(node);
        }

        private static bool NeedToTransform(ParameterExpression parameter, ConstantExpression constant)
        {
            if (parameter != null && constant != null) return parameter.Name.Equals("1") || constant.Value.Equals(1);

            return parameter?.Name.Equals("1") ?? (constant?.Value.Equals(1) ?? false);
        }


        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return node.Parameters.Any(p => values.ContainsKey(p.Name))
                ? Visit(Expression.Lambda(node.Body, node.Parameters.Where(p => !values.ContainsKey(p.Name))))
                : base.VisitLambda(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            var exp = values.ContainsKey(node.Name)
                ? Expression.Constant(values[node.Name])
                : base.VisitParameter(node);
            return exp;
        }
    }
}