using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Z3;
using ZedZedZed.Containers;

namespace ZedZedZed.Extensions
{
    public static class BoolExpressionContextExtensions
    {
        #region MkConstraint overloads
        [NotNull]
        public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Expression<Func<bool>> expression)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));

            if (expression == null) throw new ArgumentNullException(nameof(expression));

            throw new NotImplementedException();
            //return CreateBoolExpression(ctx, new Dictionary<string, BoolExpr>(), expression.Body);
        }

        [NotNull]
        public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly a, [NotNull] Expression<Func<bool, bool>> expression)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            throw new NotImplementedException();
            //return CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(expression.Parameters, a.ToExpression(ctx)), expression.Body);
        }

        [NotNull]
        public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly a, [NotNull] Booly b, [NotNull] Expression<Func<long, long, bool>> expression)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            //throw new NotImplementedException();
            return CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(expression.Parameters, a.ToExpression(ctx), b.ToExpression(ctx)), expression.Body);
        }
        #endregion

        [NotNull]
        private static BoolExpr CreateBoolExpression([NotNull] Context ctx, IReadOnlyDictionary<string, IntExpr> values, [NotNull] Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.AndAlso:
                case ExpressionType.And:
                    return ctx.MkAnd(
                        CreateBoolExpression(ctx, values, ((BinaryExpression)expression).Left),
                        CreateBoolExpression(ctx, values, ((BinaryExpression)expression).Right)
                    );

                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return ctx.MkOr(
                        CreateBoolExpression(ctx, values, ((BinaryExpression)expression).Left),
                        CreateBoolExpression(ctx, values, ((BinaryExpression)expression).Right)
                    );

                case ExpressionType.ExclusiveOr:
                    return ctx.MkXor(
                        CreateBoolExpression(ctx, values, ((BinaryExpression)expression).Left),
                        CreateBoolExpression(ctx, values, ((BinaryExpression)expression).Right)
                    );

                // a ? b : c
                // It's unclear how this can be fit into the C# syntax of expressions
                case ExpressionType.Conditional:
                    var c = (ConditionalExpression)expression;
                    return (BoolExpr)ctx.MkITE(
                        CreateBoolExpression(ctx, values, c.Test),
                        CreateBoolExpression(ctx, values, c.IfTrue),
                        CreateBoolExpression(ctx, values, c.IfFalse)
                    );

                case ExpressionType.Constant:
                    return ctx.MkBool(Convert.ToBoolean(((ConstantExpression)expression).Value));

                case ExpressionType.Equal:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThan:
                case ExpressionType.GreaterThan:
                    throw new NotImplementedException();
                    //return CreateInequality(ctx, values, (BinaryExpression)expression);

                default:
                    throw new ArgumentException($"Unknown expression type '{expression.NodeType}'");
            }
        }
    }
}
