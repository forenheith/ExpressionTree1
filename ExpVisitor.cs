// 2018032823:41ExpVisitor.csРоман Евсеев2018032823:41

using System.Linq.Expressions;

namespace ExpressionTree1
{
    public class ExpVisitor : ExpressionVisitor
    {
       public Expression ConverTo(BinaryExpression binaryExpression)
        {
            if (binaryExpression.NodeType.Equals(ExpressionType.Add))
            {
                var left = this.Visit(binaryExpression.Left);
                return Expression.MakeUnary(ExpressionType.Increment, left, typeof(int));
            }
            if (!binaryExpression.NodeType.Equals(ExpressionType.Subtract)) return null;
            {
                var left = this.Visit(binaryExpression.Left);
                return Expression.MakeUnary(ExpressionType.Decrement, left, typeof(int));
            }
        }
    }
}