using System;
using Microsoft.Z3;

namespace ZedZedZed.Containers
{
    public class Booly
        : IZ3Container
    {
        private readonly bool _value;
        private readonly BoolExpr? _expr;

        private Booly(BoolExpr expr)
        {
            _expr = expr;
        }

        private Booly(bool value)
        {
            _value = value;
        }

        public static implicit operator Booly(BoolExpr expr)
        {
            return new Booly(expr);
        }

        public static implicit operator Booly(bool value)
        {
            return new Booly(value);
        }

        public static implicit operator bool(Booly value)
        {
            throw new NotSupportedException();
        }

        public Expr ToExpr(Context ctx)
        {
            return _expr ?? ctx.MkBool(_value);
        }
    }
}
