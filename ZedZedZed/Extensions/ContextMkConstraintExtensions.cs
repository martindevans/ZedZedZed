
using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using Microsoft.Z3;
using ZedZedZed.Containers;

namespace ZedZedZed.Extensions {
	public static class MkConstraintExtensions {
				[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0, [NotNull] Expression<Func<long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0, [NotNull] Expression<Func<bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0,[NotNull] Inty p1, [NotNull] Expression<Func<long,long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0,[NotNull] Booly p1, [NotNull] Expression<Func<long,bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0,[NotNull] Inty p1, [NotNull] Expression<Func<bool,long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0,[NotNull] Booly p1, [NotNull] Expression<Func<bool,bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0,[NotNull] Inty p1,[NotNull] Inty p2, [NotNull] Expression<Func<long,long,long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0,[NotNull] Inty p1,[NotNull] Booly p2, [NotNull] Expression<Func<long,long,bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0,[NotNull] Booly p1,[NotNull] Inty p2, [NotNull] Expression<Func<long,bool,long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0,[NotNull] Booly p1,[NotNull] Booly p2, [NotNull] Expression<Func<long,bool,bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0,[NotNull] Inty p1,[NotNull] Inty p2, [NotNull] Expression<Func<bool,long,long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0,[NotNull] Inty p1,[NotNull] Booly p2, [NotNull] Expression<Func<bool,long,bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0,[NotNull] Booly p1,[NotNull] Inty p2, [NotNull] Expression<Func<bool,bool,long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0,[NotNull] Booly p1,[NotNull] Booly p2, [NotNull] Expression<Func<bool,bool,bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0,[NotNull] Inty p1,[NotNull] Inty p2,[NotNull] Inty p3, [NotNull] Expression<Func<long,long,long,long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0,[NotNull] Inty p1,[NotNull] Inty p2,[NotNull] Booly p3, [NotNull] Expression<Func<long,long,long,bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0,[NotNull] Inty p1,[NotNull] Booly p2,[NotNull] Inty p3, [NotNull] Expression<Func<long,long,bool,long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0,[NotNull] Inty p1,[NotNull] Booly p2,[NotNull] Booly p3, [NotNull] Expression<Func<long,long,bool,bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0,[NotNull] Booly p1,[NotNull] Inty p2,[NotNull] Inty p3, [NotNull] Expression<Func<long,bool,long,long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0,[NotNull] Booly p1,[NotNull] Inty p2,[NotNull] Booly p3, [NotNull] Expression<Func<long,bool,long,bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0,[NotNull] Booly p1,[NotNull] Booly p2,[NotNull] Inty p3, [NotNull] Expression<Func<long,bool,bool,long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Inty p0,[NotNull] Booly p1,[NotNull] Booly p2,[NotNull] Booly p3, [NotNull] Expression<Func<long,bool,bool,bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0,[NotNull] Inty p1,[NotNull] Inty p2,[NotNull] Inty p3, [NotNull] Expression<Func<bool,long,long,long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0,[NotNull] Inty p1,[NotNull] Inty p2,[NotNull] Booly p3, [NotNull] Expression<Func<bool,long,long,bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0,[NotNull] Inty p1,[NotNull] Booly p2,[NotNull] Inty p3, [NotNull] Expression<Func<bool,long,bool,long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0,[NotNull] Inty p1,[NotNull] Booly p2,[NotNull] Booly p3, [NotNull] Expression<Func<bool,long,bool,bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0,[NotNull] Booly p1,[NotNull] Inty p2,[NotNull] Inty p3, [NotNull] Expression<Func<bool,bool,long,long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0,[NotNull] Booly p1,[NotNull] Inty p2,[NotNull] Booly p3, [NotNull] Expression<Func<bool,bool,long,bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0,[NotNull] Booly p1,[NotNull] Booly p2,[NotNull] Inty p3, [NotNull] Expression<Func<bool,bool,bool,long, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							[NotNull] public static BoolExpr MkConstraint([NotNull] this Context ctx, [NotNull] Booly p0,[NotNull] Booly p1,[NotNull] Booly p2,[NotNull] Booly p3, [NotNull] Expression<Func<bool,bool,bool,bool, bool>> expression) {
				if (ctx == null) throw new ArgumentNullException(nameof(ctx));if (p0 == null) throw new ArgumentNullException(nameof(p0)); if (p1 == null) throw new ArgumentNullException(nameof(p1)); if (p2 == null) throw new ArgumentNullException(nameof(p2)); if (p3 == null) throw new ArgumentNullException(nameof(p3));if (expression == null) throw new ArgumentNullException(nameof(expression));
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
			
	}	//class
}	//namespace