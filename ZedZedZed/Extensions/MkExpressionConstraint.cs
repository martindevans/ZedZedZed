using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Z3;

namespace ZedZedZed.Extensions
{
    internal static class MkExpressionConstraint
    {
        [NotNull]
        internal static IReadOnlyDictionary<string, IntExpr> ExtractParameters([NotNull, ItemNotNull] ICollection<ParameterExpression> parameters, [NotNull, ItemNotNull] params IntExpr[] variables)
        {
            if (variables.Length != parameters.Count)
                throw new ArgumentException($"Expected {parameters.Count} parameters, found {variables.Length}");

            // Create map from name to parameter
            var nameToExpr = parameters
                .Select((p, i) => new { name = p.Name, variable = variables[i] })
                .ToDictionary(a => a.name, a => a.variable);

            // Find all expressions bound to multiple names.
            // We don't want to bind the same expression to multiple names because this can lead to very difficult to debug problems
            var exprToNames = parameters
                .Select((p, i) => new { name = p.Name, variable = variables[i] })
                .GroupBy(a => a.variable)
                .Where(g => g.Count() > 1)
                .ToArray();

            if (exprToNames.Any())
            {
                var problem = string.Join(", ", exprToNames.Select(a => $"{a.Key}=>[{string.Join(",", a.Select(b => b.name))}]"));
                throw new InvalidOperationException($"Attempted to bind {exprToNames.Length} expressions to multiple names: {problem}");
            }

            return nameToExpr;
        }
    }
}
