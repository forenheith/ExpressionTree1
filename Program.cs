using System;
using System.Linq.Expressions;

namespace ExpressionTree1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var addExp = Expression.Add(Expression.Variable(typeof(int), "17"), Expression.Constant(1));
            var subExp = Expression.Subtract(Expression.Variable(typeof(int), "7"), Expression.Constant(1));
            var divExp = Expression.Divide(addExp, subExp);
            Console.WriteLine($"Initial state: {divExp}");

            var convertedDiv = new ExpVisitor().Visit(divExp);
            Console.WriteLine($"Converted state: {convertedDiv}");

            Console.ReadKey();
        }
    }
}