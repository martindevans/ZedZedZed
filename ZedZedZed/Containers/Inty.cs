using System;
using Microsoft.Z3;

namespace ZedZedZed.Containers
{
    public class Inty
        : IZ3Container
    {
        private readonly long _value;
        private readonly IntExpr? _expr;

        private Inty(IntExpr expr)
        {
            _expr = expr;
        }

        private Inty(long value)
        {
            _value = value;
        }

        public static implicit operator Inty(IntExpr expr)
        {
            return new Inty(expr);
        }

        public static implicit operator Inty(long value)
        {
            return new Inty(value);
        }

        public static implicit operator long(Inty value)
        {
            throw new NotSupportedException();
        }

        public Expr ToExpr(Context ctx)
        {
            return _expr ?? ctx.MkInt(_value);
        }
    }
}
