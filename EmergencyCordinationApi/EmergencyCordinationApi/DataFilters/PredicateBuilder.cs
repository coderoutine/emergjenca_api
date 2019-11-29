using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EmergencyCordinationApi.DataFilters
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
    public static class ParameterReplacer
    {
        // Produces an expression identical to 'expression'
        // except with 'source' parameter replaced with 'target' expression.     
        public static Expression Replace(Expression expression, ParameterExpression oldParameter, Expression newBody)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (oldParameter == null) throw new ArgumentNullException(nameof(oldParameter));
            if (newBody == null) throw new ArgumentNullException(nameof(newBody));
            if (expression is LambdaExpression) throw new InvalidOperationException("The search & replace operation must be performed on the body of the lambda.");
            return (new ParameterReplacerVisitor(oldParameter, newBody)).Visit(expression);
        }

        //Chains two lambda expressions together as in the following example:
        //given these inputs:
        //  parentExpression = customer => customer.PrimaryAddress;
        //  childExpression = address => address.Street;
        //produces:
        //  customer => customer.PrimaryAddress.Street;
        //this function only supports parents with a single input parameter, and children with a single output parameter
        //many more overloads could be added for other common delegate types
        public static Expression<Func<A, C>> ChainWith<A, B, C>(this Expression<Func<A, B>> parentExpression, Expression<Func<B, C>> childExpression)
        {
            //could call Chain, but some of the checks are unnecessary since the inputs are strongly typed
            if (parentExpression == null) throw new ArgumentNullException(nameof(parentExpression));
            if (childExpression == null) throw new ArgumentNullException(nameof(childExpression));
            //since the lambda is strongly defined, we can be sure that there exists one and only one parameter on the parent and child expressions
            return Expression.Lambda<Func<A, C>>(
                Replace(childExpression.Body, childExpression.Parameters[0], parentExpression.Body),
                parentExpression.Parameters);
        }

        //Chains two lambda expressions together as in the following example:
        //given these inputs:
        //  parentExpression = (customers, index) => customers[index].PrimaryAddress;
        //  childExpression = address => Console.WriteLine(address.Street);
        //produces:
        //  (customers, index) => Console.WriteLine(customers[index].PrimaryAddress.Street);
        //this function supports parent expressions with any number of input parameters (including 0), and child expressions with no output value (Action<>s)
        //however, it is not strongly typed, and validity cannot be verified at compile time
        public static LambdaExpression Chain(LambdaExpression parentExpression, LambdaExpression childExpression)
        {
            if (parentExpression == null) throw new ArgumentNullException(nameof(parentExpression));
            if (childExpression == null) throw new ArgumentNullException(nameof(childExpression));
            if (parentExpression.ReturnType.Equals(typeof(void))) throw new ArgumentException("The parent expression must return a value.", nameof(parentExpression));
            if (childExpression.Parameters.Count != 1 || !childExpression.Parameters[0].Type.Equals(parentExpression.ReturnType)) throw new ArgumentException("The child expression must have a single parameter of the same type as the parent expression's return type.", nameof(childExpression));
            //this code could provide add a conversion between compatible types, but for now just throws an error; the types must be identical
            return Expression.Lambda(Replace(childExpression.Body, childExpression.Parameters[0], parentExpression.Body), parentExpression.Parameters);
        }

        private class ParameterReplacerVisitor : ExpressionVisitor
        {
            private ParameterExpression _source;
            private Expression _target;

            public ParameterReplacerVisitor(ParameterExpression source, Expression target)
            {
                _source = source;
                _target = target;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                // Replace the source with the target, visit other params as usual.
                return node.Equals(_source) ? _target : base.VisitParameter(node);
            }
        }
    }
}
