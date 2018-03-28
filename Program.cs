using System;
using System.Linq.Expressions;

namespace ExpressionTree1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var addExp = Expression.Add(Expression.Variable(typeof(int), "5"), Expression.Constant(1));
            var subExp = Expression.Subtract(Expression.Variable(typeof(int), "5"), Expression.Constant(1));
            var divExp = Expression.Divide(addExp, subExp);
            Console.WriteLine($"Initial state: {divExp}");

            var convertedAdd = new ExpVisitor().ConverTo(addExp);
            var convertedSub = new ExpVisitor().ConverTo(subExp);
            divExp = Expression.Divide(convertedAdd, convertedSub);
            Console.WriteLine($"Converted state: {divExp}");

            Console.ReadKey();
        }
    }
}