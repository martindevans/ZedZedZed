using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Z3;
using ZedZedZed.Extensions;

namespace ZedZedZed.Test
{
    [TestClass]
    public class IntExpressionContextExtensionsTests
    {
        //[TestMethod]
        //public void Playground()
        //{
        //    using (var ctx = new Context(new Dictionary<string, string> { { "model", "true" } }))
        //    using (var sudoku = new Sudoku(ctx))
        //        sudoku.ExpressionRun();
        //}

        [TestMethod]
        public void AssertThat_MkConstraint_EqualsParameter()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstInt("A");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, 3, (x, y) => x == -y));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var result = (IntNum)s.Model.Eval(a);
                Assert.AreEqual(-3, result.Int);
            }
        }

        [TestMethod]
        public void AssertThat_MkConstraint_EqualsConstant()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstInt("A");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, x => x == -3));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var result = (IntNum)s.Model.Eval(a);
                Assert.AreEqual(-3, result.Int);
            }
        }

        [TestMethod]
        public void AssertThat_MkConstraint_EqualsAddition()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstInt("A");
                var b = ctx.MkConstInt("B");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, b, (x, y) => x + y == 3));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var ar = ((IntNum)s.Model.Eval(a)).Int;
                var br = ((IntNum)s.Model.Eval(b)).Int;

                Assert.AreEqual(3, ar + br);
            }
        }

        [TestMethod]
        public void AssertThat_MkConstraint_EqualsSubtraction()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstInt("A");
                var b = ctx.MkConstInt("B");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, b, (x, y) => x - y == 3));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var ar = ((IntNum)s.Model.Eval(a)).Int;
                var br = ((IntNum)s.Model.Eval(b)).Int;

                Assert.AreEqual(3, ar - br);
            }
        }

        [TestMethod]
        public void AssertThat_MkConstraint_EqualsMultiplication()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstInt("A");
                var b = ctx.MkConstInt("B");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, b, (x, y) => x * y == 3));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var ar = ((IntNum)s.Model.Eval(a)).Int;
                var br = ((IntNum)s.Model.Eval(b)).Int;

                Assert.AreEqual(3, ar * br);
            }
        }

        [TestMethod]
        public void AssertThat_MkConstraint_EqualsDivision()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstInt("A");
                var b = ctx.MkConstInt("B");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, b, (x, y) => x / y == 3));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var ar = ((IntNum)s.Model.Eval(a)).Int;
                var br = ((IntNum)s.Model.Eval(b)).Int;

                Assert.AreEqual(3, ar / br);
            }
        }

        [TestMethod]
        public void AssertThat_MkConstraint_EqualsConstant_WithCastToInt32()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstInt("A");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, 3, (x, y) => x == (int)y));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var result = (IntNum)s.Model.Eval(a);
                Assert.AreEqual(3, result.Int);
            }
        }

        [TestMethod]
        public void AssertThat_MkConstraint_EqualsConstant_WithCastToUInt16()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstInt("A");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, int.MaxValue, (x, y) => x == (ushort)y));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var result = (IntNum)s.Model.Eval(a);
                Assert.AreEqual(int.MaxValue, result.Int);
            }
        }

        [TestMethod]
        public void AssertThat_MkConstraint_And()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstInt("A");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, x => x > 3 && x < 10));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var result = (IntNum)s.Model.Eval(a);
                Assert.IsTrue(result.Int > 3);
                Assert.IsTrue(result.Int < 10);
            }
        }

        [TestMethod]
        public void AssertThat_MkConstraint_Or()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstInt("A");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, x => x < 3 || x > 10));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var result = (IntNum)s.Model.Eval(a);
                Assert.IsTrue(result.Int < 3 || result.Int > 10);
            }
        }

        [TestMethod]
        public void AssertThat_MkConstraint_Xor()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstInt("A");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, x => x < 3 ^ x < 10));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var result = (IntNum)s.Model.Eval(a);
                Assert.IsTrue(result.Int < 3 ^ result.Int < 10);
            }
        }

        [TestMethod]
        public void AssertThat_MkConstraint_Mod()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstInt("A");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, x => x % 77 == 17 && x > 17));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var resulta = (IntNum)s.Model.Eval(a);

                Assert.AreEqual(17, resulta.Int % 77);
            }
        }

        [TestMethod]
        public void AssertThat_MkConstraint_NestedIfThenElse()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstInt("A");
                var b = ctx.MkConstInt("B");
                var s = ctx.MkSolver();

                // ReSharper disable once SimplifyConditionalTernaryExpression
                s.Assert(ctx.MkConstraint(a, b, (x, y) => y == ((x < 3 ? false : true) ? 2 : 4)));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var resulta = (IntNum)s.Model.Eval(a);
                var resultb = (IntNum)s.Model.Eval(b);

                if (resulta.Int < 3)
                    Assert.AreEqual(4, resultb.Int);
                if (resulta.Int > 3)
                    Assert.AreEqual(2, resultb.Int);
            }
        }

        [TestMethod]
        public void AssertThat_MkConstraint_ThrowsWithMultipleNameBindings()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstInt("A");
                var s = ctx.MkSolver();

                Assert.ThrowsException<InvalidOperationException>(() => {
                    // ReSharper disable once AccessToDisposedClosure
                    s.Assert(ctx.MkConstraint(a, a, (x, y) => x != y));
                });
            }
        }

        //[TestMethod]
        //public void AssertThat_MkConstraint_Power()
        //{
        //    using (var ctx = new Context())
        //    {
        //        var a = ctx.MkConstInt("A");
        //        var s = ctx.MkSolver();

        //        //C# does now have an exponentiation operator, so how do we represent this in an expression?
        //        s.Assert(ctx.MkConstraint(a, x => x ^ 7 == 49));

        //        Assert.AreEqual(Status.SATISFIABLE, s.Check());

        //        var resulta = (IntNum)s.Model.Eval(a);

        //        Assert.AreEqual(7, resulta.Int);
        //    }
        //}
    }
}
