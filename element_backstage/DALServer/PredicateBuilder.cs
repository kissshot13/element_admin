﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;

namespace DALServer
{

    public static class PredicateBuilder
    {
        public static Expression<Func<T,bool>> True<T>()
        {
            return f => true;
        }


        public static Expression<Func<T,bool>> Fasle<T>()
        {
            return f => false;
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T,bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Add);
        }

        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

    }

    public class ParameterRebinder: ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder (Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map,Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            ParameterExpression replacement;
            if (map.TryGetValue(node, out replacement))
            {
                node = replacement;
            }
            return base.VisitParameter(node);

        }
    }
}
