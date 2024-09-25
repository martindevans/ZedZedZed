using Microsoft.Z3;
using ZedZedZed.Containers;
using ZedZedZed.Extensions;

namespace ZedZedZed.Test
{
    [TestClass]
    public class Playground
    {
        [TestMethod]
        public void Superoptimization()
        {
            // types:
            // 0 - int
            // 1 - str

            /*
             * a = :a
             * b = :b
             * conditional(a < b) // let's assume comparing strings is an error
             * true {
             *  :c = a
             * }
             * false {
             *  :c = b
             * }
             * error {
             *  :c = 0
             * }
             */

            using (var ctx = new Context())
            {
                var s = ctx.MkSolver();
                
                var typeSort = ctx.MkEnumSort(ctx.MkSymbol("type"), ctx.MkSymbol("int"), ctx.MkSymbol("str"));
                var itype = typeSort.Const(0);
                var stype = typeSort.Const(1);

                var a0_typ = ctx.MkConst("a0_typ", typeSort);
                var a0_int = (IntExpr)ctx.MkConst("a0_int", ctx.IntSort);
                var a0_str = ctx.MkConst("a0_str", ctx.StringSort);

                var b0_typ = ctx.MkConst("b0_typ", typeSort);
                var b0_int = (IntExpr)ctx.MkConst("b0_int", ctx.IntSort);
                var b0_str = ctx.MkConst("b0_str", ctx.StringSort);

                // A is a number value
                s.Assert(ctx.MkEq(a0_typ, typeSort.Const(0)));

                // B is a number value
                s.Assert(ctx.MkEq(b0_typ, typeSort.Const(0)));

                // branch can go three ways:
                // 0: a < b
                // 1: b > a
                // 2: either is a string
                var branch0 = ctx.MkConst("branch0", ctx.IntSort);

                s.Assert(
                      (ctx.MkEq(a0_typ, itype) & ctx.MkEq(b0_typ, itype) & ctx.MkLt(a0_int, b0_int) & ctx.MkEq(branch0, ctx.MkInt(0)))
                    | (ctx.MkEq(a0_typ, itype) & ctx.MkEq(b0_typ, itype) & ctx.MkNot(ctx.MkLt(a0_int, b0_int)) & ctx.MkEq(branch0, ctx.MkInt(1)))
                    | ctx.MkEq(branch0, ctx.MkInt(1))
                );

                s.Push();
                s.Assert(ctx.MkEq(branch0, ctx.MkInt(2)));
                Console.WriteLine(s.Check());
                s.Pop();
                Console.WriteLine(s.Check());

                Console.WriteLine(s.Check());
                var v = s.Model.Eval(branch0);
                Console.WriteLine(v);

                ////https://stackoverflow.com/questions/13395391/z3-finding-all-satisfying-models
                //var e = s.Model.ConstInterp(b0_type.FuncDecl);
                //Console.WriteLine(e);
                ////s.Model.ConstInterp(
            }
        }

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
