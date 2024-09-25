using Microsoft.Z3;
using ZedZedZed.Extensions;

namespace ZedZedZed.Test
{
    [TestClass]
    public class BoolExpressionContextExtensionsTests
    {
        [TestMethod]
        public void MkConstraint_EqualsParameter()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstBool("A");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, true, (x, y) => x == y));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var result = (BoolExpr)s.Model.Eval(a);
                Assert.IsTrue(result.IsTrue);
            }
        }

        [TestMethod]
        public void MkConstraint_EqualsConstant()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstBool("A");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, x => x == false));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var result = (BoolExpr)s.Model.Eval(a);
                Assert.IsFalse(result.IsTrue);
            }
        }

        [TestMethod]
        public void MkConstraint_Not()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstBool("A");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, x => !x));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var ar = s.Model.Eval(a).IsTrue;

                Assert.IsFalse(ar);
            }
        }

        [TestMethod]
        public void MkConstraint_And()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstBool("A");
                var b = ctx.MkConstBool("B");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, b, (x, y) => x && !y));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var ar = s.Model.Eval(a).IsTrue;
                var br = s.Model.Eval(b).IsTrue;

                Assert.IsTrue(ar && !br);
            }
        }

        [TestMethod]
        public void MkConstraint_Xor()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstBool("A");
                var b = ctx.MkConstBool("B");
                var s = ctx.MkSolver();

                // Set up:
                //    UNSATISFIABLE XOR SOMETHING
                // Then Verify that `SOMETHING` holds
                s.Assert(ctx.MkConstraint(a, b, (x, y) => (x && !x && !y) ^ (x && y)));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var ar = s.Model.Eval(a).IsTrue;
                var br = s.Model.Eval(b).IsTrue;

                Assert.IsTrue(ar && br);
            }
        }

        [TestMethod]
        public void MkConstraint_Or()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstBool("A");
                var b = ctx.MkConstBool("B");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, b, (x, y) => (x && y) || (!x && !y)));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var ar = s.Model.Eval(a).IsTrue;
                var br = s.Model.Eval(b).IsTrue;

                Assert.IsTrue(ar && br || !ar && !br);
            }
        }

        [TestMethod]
        public void MkConstraint_Conditional()
        {
            using (var ctx = new Context())
            {
                var a = ctx.MkConstBool("A");
                var b = ctx.MkConstBool("B");
                var s = ctx.MkSolver();

                s.Assert(ctx.MkConstraint(a, b, (aa, bb) => aa ? bb : bb && aa));

                Assert.AreEqual(Status.SATISFIABLE, s.Check());

                var ar = s.Model.Eval(a).IsTrue;
                var br = s.Model.Eval(b).IsTrue;

                Assert.IsTrue(ar ? br : br && ar);
            }
        }
    }
}
