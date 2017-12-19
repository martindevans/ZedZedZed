using System;
using JetBrains.Annotations;
using Microsoft.Z3;

namespace ZedZedZed.Containers
{
    public class Inty
        : IZ3Container
    {
        private readonly long _value;
        private readonly IntExpr _expr;

        private Inty(IntExpr expr)
        {
            _expr = expr;
        }

        private Inty(long value)
        {
            _value = value;
        }

        [NotNull] public static implicit operator Inty(IntExpr expr)
        {
            return new Inty(expr);
        }

        [NotNull] public static implicit operator Inty(long value)
        {
            return new Inty(value);
        }

        public static implicit operator long(Inty value)
        {
            throw new NotSupportedException();
        }

        [NotNull]
        public Expr ToExpr(Context ctx)
        {
            if (_expr != null)
                return _expr;
            else
                return ctx.MkInt(_value);
        }
    }
}
