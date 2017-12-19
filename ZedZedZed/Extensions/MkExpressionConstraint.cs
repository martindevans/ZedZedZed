using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Z3;
using ZedZedZed.Containers;

namespace ZedZedZed.Extensions
{
    internal static class MkExpressionConstraint
    {
        [NotNull]
        internal static IReadOnlyDictionary<string, Expr> ExtractParameters(Context ctx, [NotNull, ItemNotNull] ICollection<ParameterExpression> parameters, [NotNull, ItemNotNull] params IZ3Container[] containers)
        {
            var values = containers.Select(c => c.ToExpr(ctx)).ToArray();
            if (values.Length != parameters.Count)
                throw new ArgumentException($"Expected {parameters.Count} parameters, found {values.Length}");

            // Create map from name to parameter
            var nameToExpr = parameters
                .Select((p, i) => new { name = p.Name, variable = values[i] })
                .ToDictionary(a => a.name, a => a.variable);

            // Find all expressions bound to multiple names.
            // We don't want to bind the same expression to multiple names because this can lead to very difficult to debug problems
            var exprToNames = parameters
                .Select((p, i) => new { name = p.Name, variable = values[i] })
                .GroupBy(a => a.variable)
                .ToArray();

            if (exprToNames.Any(g => g.Count() > 1))
            {
                var problem = string.Join(", ", exprToNames.Select(a => $"{a.Key}=>[{string.Join(",", a.Select(b => b.name))}]"));
                throw new InvalidOperationException($"Attempted to bind {exprToNames.Length} expressions to multiple names: {problem}");
            }

            return nameToExpr;
        }

        [NotNull]
        internal static BoolExpr CreateBoolExpression([NotNull] Context ctx, IReadOnlyDictionary<string, Expr> values, [NotNull] Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Not:
                    return ctx.MkNot(CreateBoolExpression(ctx, values, ((UnaryExpression)expression).Operand));

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

                case ExpressionType.Conditional:
                    var c = (ConditionalExpression)expression;
                    return (BoolExpr)ctx.MkITE(
                        CreateBoolExpression(ctx, values, c.Test),
                        CreateBoolExpression(ctx, values, c.IfTrue),
                        CreateBoolExpression(ctx, values, c.IfFalse)
                    );

                case ExpressionType.Constant:
                    return ctx.MkBool(Convert.ToBoolean(((ConstantExpression)expression).Value));

                case ExpressionType.Parameter:
                    return (BoolExpr)values[((ParameterExpression)expression).Name];

                case ExpressionType.Convert:
                    return CreateBoolExpression(ctx, values, ((UnaryExpression)expression).Operand);

                case ExpressionType.NotEqual:
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

        private static bool IsDecimalType(Type type)
        {
            return type == typeof(float)
                || type == typeof(double)
                || type == typeof(decimal);
        }

        private static bool IsIntegerType(Type type)
        {
            return type == typeof(byte)
                || type == typeof(short)
                || type == typeof(int)
                || type == typeof(long);
        }

        private static bool IsBoolType(Type type)
        {
            return type == typeof(bool);
        }

        private static BoolExpr CreateInequality([NotNull] Context ctx, [NotNull] IReadOnlyDictionary<string, Expr> values, [NotNull] BinaryExpression expression)
        {
            //If the two sides are both booleans then we can only do direct equality tests
            if (IsBoolType(expression.Left.Type) && IsBoolType(expression.Right.Type))
            {
                var eq = ctx.MkEq(CreateBoolExpression(ctx, values, expression.Left), CreateBoolExpression(ctx, values, expression.Right));
                if (expression.NodeType == ExpressionType.Equal)
                    return eq;
                else if (expression.NodeType == ExpressionType.NotEqual)
                    return ctx.MkNot(eq);
                else
                    throw new ArgumentException($"Unknown bool (in)equality expression type '{expression.NodeType}'");
            }

            if (IsDecimalType(expression.Left.Type) && IsDecimalType(expression.Right.Type))
                //return DecimalExpressionContextExtensions.CreateIntInequality(ctx, values, expression);
                throw new NotImplementedException("Decimal inequality");

            if (IsIntegerType(expression.Left.Type) && IsIntegerType(expression.Right.Type))
                return IntExpressionContextExtensions.CreateIntInequality(ctx, values, expression);

            throw new ArgumentException($"Unknown expression type '{expression.NodeType}'");
        }
    }
}
