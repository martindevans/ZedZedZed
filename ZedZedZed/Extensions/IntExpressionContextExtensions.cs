using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Z3;

namespace ZedZedZed.Extensions
{
    public static class IntExpressionContextExtensions
    {
        [NotNull]
        internal static BoolExpr CreateIntInequality([NotNull] Context ctx, [NotNull] IReadOnlyDictionary<string, Expr> values, [NotNull] BinaryExpression expression)
        {
            var left = CreateIntExpression(ctx, values, expression.Left);
            var right = CreateIntExpression(ctx, values, expression.Right);

            switch (expression.NodeType)
            {
                case ExpressionType.NotEqual:
                    return ctx.MkNot(ctx.MkEq(left, right));

                case ExpressionType.Equal:
                    return ctx.MkEq(left, right);

                case ExpressionType.GreaterThan:
                    return ctx.MkGt(left, right);

                case ExpressionType.GreaterThanOrEqual:
                    return ctx.MkGe(left, right);

                case ExpressionType.LessThan:
                    return ctx.MkLt(left, right);

                case ExpressionType.LessThanOrEqual:
                    return ctx.MkLe(left, right);

                default:
                    throw new NotSupportedException($"Unsupported inequality operator \"{expression.NodeType}\"");
            }
        }

        [NotNull]
        private static IntExpr CreateIntExpression(Context ctx, [NotNull] IReadOnlyDictionary<string, Expr> variables, [NotNull] Expression expression)
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
                    return (IntExpr)variables[((ParameterExpression)expression).Name];

                case ExpressionType.Constant:
                    return ctx.MkInt(Convert.ToInt64(((ConstantExpression)expression).Value));

                case ExpressionType.Convert:
                    return CreateIntExpression(ctx, variables, ((UnaryExpression)expression).Operand);

                case ExpressionType.Conditional:
                    {
                        var c = (ConditionalExpression)expression;
                        return (IntExpr)ctx.MkITE(
                            MkExpressionConstraint.CreateBoolExpression(ctx, variables, c.Test),
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
