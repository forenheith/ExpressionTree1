// 2018032823:41ExpVisitor.csРоман Евсеев2018032823:41

using System.Linq.Expressions;

namespace ExpressionTree1
{
    public class ExpVisitor : ExpressionVisitor
    {
        public Expression Visit(BinaryExpression binaryExpression)
        {
            return VisitBinary(binaryExpression);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.Equals(null))
                return base.VisitBinary(node);

            if (!node.NodeType.Equals(ExpressionType.Add) || !node.NodeType.Equals(ExpressionType.Subtract))
                return base.VisitBinary(node);

            ConstantExpression constant = null;
            if (node.Left.NodeType.Equals(ExpressionType.Constant) ||
                node.Right.NodeType.Equals(ExpressionType.Constant))
            {
                constant = node.Left.NodeType.Equals(ExpressionType.Constant)
                    ? (ConstantExpression) node.Left
                    : (ConstantExpression) node.Right;
            }

            ParameterExpression parameter = null;
            if (node.Left.NodeType.Equals(ExpressionType.Parameter) ||
                node.Right.NodeType.Equals(ExpressionType.Parameter))
            {
                parameter = node.Left.NodeType.Equals(ExpressionType.Parameter)
                    ? (ParameterExpression) node.Left
                    : (ParameterExpression) node.Right;
            }
            if (!NeedToTransform(parameter, constant))
                return base.VisitBinary(node);

            return node.NodeType == ExpressionType.Add ? Expression.Increment(node) : Expression.Decrement(node);
        }

        private static bool NeedToTransform(ParameterExpression parameter, ConstantExpression constant)
        {
            if (parameter != null && constant != null)
            {
                return (parameter.Name.Equals("1") || constant.Value.Equals(1));
            }

            return parameter?.Name.Equals("1") ?? (constant?.Value.Equals(1) ?? false);
        }
    }
}