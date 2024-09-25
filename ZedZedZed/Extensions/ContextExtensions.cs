using Microsoft.Z3;

namespace ZedZedZed.Extensions
{
    public static class ContextExtensions
    {
        public static IntExpr MkConstInt(this Context ctx, string name)
        {
            return (IntExpr)ctx.MkConst(name, ctx.IntSort);
        }

        public static BoolExpr MkConstBool(this Context ctx, string name)
        {
            return (BoolExpr)ctx.MkConst(name, ctx.BoolSort);
        }
    }
}
