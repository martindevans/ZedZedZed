using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Z3;
using ZedZedZed.Containers;
using ZedZedZed.Extensions;

namespace ZedZedZed.Test
{
    [TestClass]
    public class Playground
    {
        [TestMethod]
        public void Playground1()
        {
            using (var ctx = new Context())
            {
                var b = ctx.MkConst("B", ctx.RealSort);
                var c = ctx.MkConst("C", ctx.IntSort);

                var s = ctx.MkSolver();

                // Ensure B is a multiple of 1.3
                // 1 == b / (c * 1.3)
                s.Assert(ctx.MkEq(ctx.MkReal(1), ctx.MkDiv((ArithExpr)b, ctx.MkMul(ctx.MkReal(13, 10), (ArithExpr)c))));
                s.Assert(ctx.MkConstraint((Inty)c, cc => cc != 0));

                Console.WriteLine(s.Check());

                //var ar = (IntNum)s.Model.Eval(a);
                var br = ((RatNum)s.Model.Eval(b)).AsDecimal();
                Console.WriteLine(br);
                var cr = (IntNum)s.Model.Eval(c);
                Console.WriteLine(cr);

                Console.WriteLine("1 == b / (c * 1.3)");
                Console.WriteLine("1 == {0} / ({1} * 1.3)", br, cr);
            }
        }

        #region template parts
        [TestMethod]
        public void Playground2()
        {
            var el = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("Booly", "bool"), new KeyValuePair<string, string>("Inty", "long") };
            var re = new List<KeyValuePair<string, string>[]>();
            Permutation(new KeyValuePair<string, string>[2], 0, el, re);

            foreach (var item in re)
            {
                Console.WriteLine(FormatSignature(item));
                Console.WriteLine(FormatChecks(item));
                Console.WriteLine(FormatBody(item));
                Console.WriteLine("}");

                Console.WriteLine();
            }
        }

        private static string FormatBody(IReadOnlyList<KeyValuePair<string, string>> ts)
        {
            /*
             return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, a, b, c), expression.Body);
            */

            var ps = string.Join(",", ts.Select((a, i) => $"_p{i}"));

            return $"return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, {ps}, expression.Body);";
        }

        private static string FormatChecks(IReadOnlyList<KeyValuePair<string, string>> ts)
        {
            /*
             if (ctx == null) throw new ArgumentNullException(nameof(ctx));
             if (a == null) throw new ArgumentNullException(nameof(a));
             if (b == null) throw new ArgumentNullException(nameof(b));
             if (expression == null) throw new ArgumentNullException(nameof(expression));
            */

            var paramChecks = string.Join(" ", ts.Select((a, i) => string.Format("if (_p{0} == null) throw new ArgumentNullException(nameof(_p{0}));", i)));

            return $"if (ctx == null) throw new ArgumentNullException(nameof(ctx));\n{paramChecks}\n" + "if (expression == null) throw new ArgumentNullException(nameof(expression));";
        }

        private static string FormatSignature(IReadOnlyList<KeyValuePair<string, string>> types)
        {
            /*
             [NotNull]
             public static BoolExpr MkConstraint(
               [NotNull] this Context ctx,
               [NotNull] Inty a, [NotNull] Inty b, [NotNull] Inty c,
               [NotNull] Expression<Func<long, long, long, bool>> expression
             ) {
            */

            var inputs = string.Join(",", types.Select((a, i) => string.Format("[NotNull] {0} _p{1}", a.Key, i)));
            var exprs = string.Join(",", types.Select(a => a.Value));

            return $"[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, {inputs}, [NotNull] Expression<Func<{exprs}>> expression) {{";
        }

        private static void Permutation<TE>(TE[] perm, int pos, IReadOnlyList<TE> elements, ICollection<TE[]> results)
        {

            if (pos == perm.Length)
            {
                results.Add(perm.ToArray());
            }
            else
            {
                for (int i = 0; i < elements.Count; i++)
                {
                    perm[pos] = elements[i];
                    Permutation(perm, pos + 1, elements, results);
                }
            }

        }
        #endregion
    }
}
