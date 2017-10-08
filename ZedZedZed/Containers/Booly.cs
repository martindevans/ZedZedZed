using System;
using JetBrains.Annotations;
using Microsoft.Z3;

namespace ZedZedZed.Containers
{
    public class Booly
    {
        private readonly bool _value;
        private readonly BoolExpr _expr;

        private Booly(BoolExpr expr)
        {
            _expr = expr;
        }

        private Booly(bool value)
        {
            _value = value;
        }

        [NotNull]
        public static implicit operator Booly(BoolExpr expr)
        {
            return new Booly(expr);
        }

        [NotNull]
        public static implicit operator Booly(bool value)
        {
            return new Booly(value);
        }

        public static implicit operator bool(Booly value)
        {
            throw new NotSupportedException();
        }

        [NotNull]
        public BoolExpr ToExpression(Context ctx)
        {
            if (_expr != null)
                return _expr;
            else
                return ctx.MkBool(_value);
        }
    }
}
