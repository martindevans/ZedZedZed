
using System;
using System.Linq.Expressions;
using Microsoft.Z3;
using ZedZedZed.Containers;

namespace ZedZedZed.Extensions {
	public static class MkConstraintExtensions {
				public static BoolExpr MkConstraint(this Context ctx, Inty p0, Expression<Func<long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0, Expression<Func<bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Inty p0,Inty p1, Expression<Func<long,long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Inty p0,Booly p1, Expression<Func<long,bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0,Inty p1, Expression<Func<bool,long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0,Booly p1, Expression<Func<bool,bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Inty p0,Inty p1,Inty p2, Expression<Func<long,long,long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Inty p0,Inty p1,Booly p2, Expression<Func<long,long,bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Inty p0,Booly p1,Inty p2, Expression<Func<long,bool,long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Inty p0,Booly p1,Booly p2, Expression<Func<long,bool,bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0,Inty p1,Inty p2, Expression<Func<bool,long,long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0,Inty p1,Booly p2, Expression<Func<bool,long,bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0,Booly p1,Inty p2, Expression<Func<bool,bool,long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0,Booly p1,Booly p2, Expression<Func<bool,bool,bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Inty p0,Inty p1,Inty p2,Inty p3, Expression<Func<long,long,long,long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Inty p0,Inty p1,Inty p2,Booly p3, Expression<Func<long,long,long,bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Inty p0,Inty p1,Booly p2,Inty p3, Expression<Func<long,long,bool,long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Inty p0,Inty p1,Booly p2,Booly p3, Expression<Func<long,long,bool,bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Inty p0,Booly p1,Inty p2,Inty p3, Expression<Func<long,bool,long,long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Inty p0,Booly p1,Inty p2,Booly p3, Expression<Func<long,bool,long,bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Inty p0,Booly p1,Booly p2,Inty p3, Expression<Func<long,bool,bool,long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Inty p0,Booly p1,Booly p2,Booly p3, Expression<Func<long,bool,bool,bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0,Inty p1,Inty p2,Inty p3, Expression<Func<bool,long,long,long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0,Inty p1,Inty p2,Booly p3, Expression<Func<bool,long,long,bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0,Inty p1,Booly p2,Inty p3, Expression<Func<bool,long,bool,long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0,Inty p1,Booly p2,Booly p3, Expression<Func<bool,long,bool,bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0,Booly p1,Inty p2,Inty p3, Expression<Func<bool,bool,long,long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0,Booly p1,Inty p2,Booly p3, Expression<Func<bool,bool,long,bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0,Booly p1,Booly p2,Inty p3, Expression<Func<bool,bool,bool,long, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
							public static BoolExpr MkConstraint(this Context ctx, Booly p0,Booly p1,Booly p2,Booly p3, Expression<Func<bool,bool,bool,bool, bool>> expression) {
				return MkExpressionConstraint.CreateBoolExpression(ctx, MkExpressionConstraint.ExtractParameters(ctx, expression.Parameters, p0,p1,p2,p3), expression.Body);
			}
			
	}	//class
}	//namespace