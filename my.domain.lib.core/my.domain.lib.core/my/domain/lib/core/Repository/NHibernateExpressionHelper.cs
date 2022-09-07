// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.NHibernateExpressionHelper
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using NHibernate.Criterion;
using NHibernate.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace my.domain.lib.core.Repository
{
    public class NHibernateExpressionHelper
    {
        private static bool _registered = false;

        public static void RegisterMethods()
        {
            if (NHibernateExpressionHelper._registered)
                return;
            NHibernateExpressionHelper._registered = true;
            string str = (string)null;
            ExpressionProcessor.RegisterCustomMethodCall((System.Linq.Expressions.Expression<Func<bool>>)(() => str.StartsWith(default(string))), new Func<MethodCallExpression, ICriterion>(NHibernateExpressionHelper.ProcessStringStartsWith));
            ExpressionProcessor.RegisterCustomMethodCall((System.Linq.Expressions.Expression<Func<bool>>)(() => str.EndsWith(default(string))), new Func<MethodCallExpression, ICriterion>(NHibernateExpressionHelper.ProcessStringEndsWith));
            ExpressionProcessor.RegisterCustomMethodCall((System.Linq.Expressions.Expression<Func<bool>>)(() => str.Contains(default(string))), new Func<MethodCallExpression, ICriterion>(NHibernateExpressionHelper.ProcessStringContains));
            IEnumerable<Guid> enumerable = (IEnumerable<Guid>)null;
            ExpressionProcessor.RegisterCustomMethodCall((System.Linq.Expressions.Expression<Func<bool>>)(() => enumerable.Contains<Guid>(Guid.Empty)), new Func<MethodCallExpression, ICriterion>(NHibernateExpressionHelper.ProcessEnumerableContainsGuid));
            ICollection<Guid> collection = (ICollection<Guid>)null;
            ExpressionProcessor.RegisterCustomMethodCall((System.Linq.Expressions.Expression<Func<bool>>)(() => collection.Contains(Guid.Empty)), new Func<MethodCallExpression, ICriterion>(NHibernateExpressionHelper.ProcessEnumerableContainsGuid));
        }

        private static ICriterion ProcessStringStartsWith(
          MethodCallExpression methodCallExpression)
        {
            return ExpressionProcessor.FindMemberProjection(methodCallExpression.Object).CreateCriterion(new Func<string, object, ICriterion>(Restrictions.Like), new Func<IProjection, object, ICriterion>(Restrictions.Like), (object)(ExpressionProcessor.FindValue(methodCallExpression.Arguments[0]).ToString() + "%"));
        }

        private static ICriterion ProcessStringEndsWith(
          MethodCallExpression methodCallExpression)
        {
            return ExpressionProcessor.FindMemberProjection(methodCallExpression.Object).CreateCriterion(new Func<string, object, ICriterion>(Restrictions.Like), new Func<IProjection, object, ICriterion>(Restrictions.Like), (object)("%" + ExpressionProcessor.FindValue(methodCallExpression.Arguments[0])));
        }

        private static ICriterion ProcessStringContains(
          MethodCallExpression methodCallExpression)
        {
            return ExpressionProcessor.FindMemberProjection(methodCallExpression.Object).CreateCriterion(new Func<string, object, ICriterion>(Restrictions.Like), new Func<IProjection, object, ICriterion>(Restrictions.Like), (object)("%" + ExpressionProcessor.FindValue(methodCallExpression.Arguments[0]) + "%"));
        }

        private static ICriterion ProcessEnumerableContainsGuid(
          MethodCallExpression methodCallExpression)
        {
            return (ICriterion)new InExpression(ExpressionProcessor.FindMemberProjection(methodCallExpression.Arguments[0]).AsProjection(), (ExpressionProcessor.FindValue(methodCallExpression.Object) as Guid[]).Cast<object>().ToArray<object>());
        }
    }
}
