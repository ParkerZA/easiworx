// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.SqlStatementInterceptor
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Type;
using System;

namespace my.domain.lib.core.Repository
{
    public class SqlStatementInterceptor : EmptyInterceptor, IInterceptor
    {
       
        public override void AfterTransactionBegin(ITransaction tx)
        {
            base.AfterTransactionBegin(tx);
        }

        public override void AfterTransactionCompletion(ITransaction tx)
        {
            base.AfterTransactionCompletion(tx);
        }

        public override SqlString OnPrepareStatement(SqlString sql)
        {
            return base.OnPrepareStatement(sql);
        }

       
        public override bool OnSave(
          object entity,
          object id,
          object[] state,
          string[] propertyNames,
          IType[] types)
        {
            return base.OnSave(entity, id, state, propertyNames, types);
        }

        public override bool OnFlushDirty(
          object entity,
          object id,
          object[] currentState,
          object[] previousState,
          string[] propertyNames,
          IType[] types)
        {
            return base.OnFlushDirty(entity, id, currentState, previousState, propertyNames, types);
        }

        public override bool OnLoad(
          object entity,
          object id,
          object[] state,
          string[] propertyNames,
          IType[] types)
        {
            return base.OnLoad(entity, id, state, propertyNames, types);
        }

        public override void OnDelete(
          object entity,
          object id,
          object[] state,
          string[] propertyNames,
          IType[] types)
        {
            base.OnDelete(entity, id, state, propertyNames, types);
        }

        private void MinValToNull(object[] state, IType[] types)
        {
            int index = 0;
            foreach (IType type in types)
            {
                try
                {
                    if (type.ReturnedClass == typeof(DateTime) && DateTime.MinValue.Equals(state[index]))
                        state[index] = (object)null;
                    if (type.ReturnedClass == typeof(int) && (int)state[index] == 0)
                        state[index] = (object)null;
                    if (type.ReturnedClass == typeof(Decimal) && (Decimal)state[index] == Decimal.Zero)
                        state[index] = (object)null;
                    if (type.ReturnedClass == typeof(double) && (double)state[index] == 0.0)
                        state[index] = (object)null;
                }
                catch (Exception ex)
                {
                }
                ++index;
            }
        }

        private void NullValToMinVal(object[] state, IType[] types)
        {
            int index = 0;
            foreach (IType type in types)
            {
                try
                {
                    if (type.ReturnedClass == typeof(DateTime) && state[index] == null)
                        state[index] = (object)DateTime.MinValue;
                    if (type.ReturnedClass == typeof(int) && state[index] == null)
                        state[index] = (object)0;
                    if (type.ReturnedClass == typeof(Decimal) && (state[index] == null || double.NaN.Equals(state[index])))
                        state[index] = (object)Decimal.Zero;
                    if (type.ReturnedClass == typeof(double) && (state[index] == null || double.NaN.Equals(state[index])))
                        state[index] = (object)0.0;
                    if (type.ReturnedClass == typeof(string) && state[index] == null)
                        state[index] = (object)"";
                }
                catch (Exception ex)
                {
                }
                ++index;
            }
        }
    }
}
