using Microsoft.Z3;

namespace ZedZedZed.Containers
{
    internal interface IZ3Container
    {
        Expr ToExpr(Context ctx);
    }
}
