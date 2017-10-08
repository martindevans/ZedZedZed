using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Z3;
using ZedZedZed.Containers;

namespace ZedZedZed.Extensions
{
    public static class IntExpressionContextExtensions
    {
        #region MkConstraint overloads
        [NotNull]
        public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Expression<Func<bool>> expression)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));

            if (expression == null) throw new ArgumentNullException(nameof(expression));

            return CreateBoolExpression(ctx, new Dictionary<string, IntExpr>(), expression.Body);
        }

        [NotNull]
        public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty a, [NotNull] Expression<Func<long, bool>> expression)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            return CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(expression.Parameters, a.ToExpression(ctx)), expression.Body);
        }

        [NotNull]
        public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty a, [NotNull] Inty b, [NotNull] Expression<Func<long, long, bool>> expression)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));
            if (expression == null) throw new ArgumentNullException(nameof(expression));

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
                    return CreateInequality(ctx, values, (BinaryExpression)expression);

                default:
                    throw new ArgumentException($"Unknown expression type '{expression.NodeType}'");
            }
        }

        [NotNull]
        private static BoolExpr CreateInequality([NotNull] Context ctx, [NotNull] IReadOnlyDictionary<string, IntExpr> values, [NotNull] BinaryExpression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Equal:
                    return ctx.MkEq(CreateIntExpression(ctx, values, expression.Left), CreateIntExpression(ctx, values, expression.Right));

                case ExpressionType.GreaterThan:
                    return ctx.MkGt(CreateIntExpression(ctx, values, expression.Left), CreateIntExpression(ctx, values, expression.Right));

                case ExpressionType.GreaterThanOrEqual:
                    return ctx.MkGe(CreateIntExpression(ctx, values, expression.Left), CreateIntExpression(ctx, values, expression.Right));

                case ExpressionType.LessThan:
                    return ctx.MkLt(CreateIntExpression(ctx, values, expression.Left), CreateIntExpression(ctx, values, expression.Right));

                case ExpressionType.LessThanOrEqual:
                    return ctx.MkLe(CreateIntExpression(ctx, values, expression.Left), CreateIntExpression(ctx, values, expression.Right));

                default:
                    throw new NotSupportedException($"Unsupported inequality operator \"{expression.NodeType}\"");
            }
        }

        [NotNull]
        private static IntExpr CreateIntExpression(Context ctx, [NotNull] IReadOnlyDictionary<string, IntExpr> variables, [NotNull] Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Add:
                    {
                        var b = (BinaryExpression)expression;
                        return (IntExpr)ctx.MkAdd(CreateIntExpression(ctx, variables, b.Left), CreateIntExpression(ctx, variables, b.Right));
                    }

                case ExpressionType.Subtract:
                    {
                        var b = (BinaryExpression)expression;
                        return (IntExpr)ctx.MkSub(CreateIntExpression(ctx, variables, b.Left), CreateIntExpression(ctx, variables, b.Right));
                    }

                case ExpressionType.Multiply:
                    {
                        var b = (BinaryExpression)expression;
                        return (IntExpr)ctx.MkMul(CreateIntExpression(ctx, variables, b.Left), CreateIntExpression(ctx, variables, b.Right));
                    }

                case ExpressionType.Divide:
                    {
                        var b = (BinaryExpression)expression;
                        return (IntExpr)ctx.MkDiv(CreateIntExpression(ctx, variables, b.Left), CreateIntExpression(ctx, variables, b.Right));
                    }

                case ExpressionType.Negate:
                    {
                        var u = (UnaryExpression)expression;
                        return (IntExpr)ctx.MkUnaryMinus(CreateIntExpression(ctx, variables, u.Operand));
                    }

                case ExpressionType.Modulo:
                    {
                        var b = (BinaryExpression)expression;
                        return ctx.MkMod(CreateIntExpression(ctx, variables, b.Left), CreateIntExpression(ctx, variables, b.Right));
                    }

                case ExpressionType.Parameter:
                    return variables[((ParameterExpression)expression).Name];

                case ExpressionType.Constant:
                    return ctx.MkInt(Convert.ToInt64(((ConstantExpression)expression).Value));

                case ExpressionType.Convert:
                    return CreateIntExpression(ctx, variables, ((UnaryExpression)expression).Operand);

                case ExpressionType.Conditional:
                    {
                        var c = (ConditionalExpression)expression;
                        return (IntExpr)ctx.MkITE(
                            CreateBoolExpression(ctx, variables, c.Test),
                            CreateIntExpression(ctx, variables, c.IfTrue),
                            CreateIntExpression(ctx, variables, c.IfFalse)
                        );
                    }

                default:
                    throw new ArgumentException($"Invalid node type {expression.NodeType}", nameof(expression));
            }
        }
    }
}
