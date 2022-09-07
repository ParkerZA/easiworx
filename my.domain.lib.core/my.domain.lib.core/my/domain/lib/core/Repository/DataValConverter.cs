// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.DataValConverter
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using NHibernate.Type;
using System;

namespace my.domain.lib.core.Repository
{
    internal class DataValConverter
    {
        internal void NullToMinVal(
          object entity,
          object[] state,
          string[] propertyNames,
          IType[] types)
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

        internal void MinToNullVal(
          object entity,
          object[] state,
          string[] propertyNames,
          IType[] types)
        {
            int index = 0;
            foreach (IType type in types)
            {
                try
                {
                    if (type.ReturnedClass == typeof(DateTime) && DateTime.MinValue.Equals(state[index]))
                        state[index] = (object)null;
                    if (type.ReturnedClass == typeof(int) && state[index].Equals((object)0))
                        state[index] = (object)null;
                    if (type.ReturnedClass == typeof(Decimal) && state[index].Equals((object)Decimal.Zero))
                        state[index] = (object)null;
                    if (type.ReturnedClass == typeof(double) && state[index].Equals((object)0.0))
                        state[index] = (object)null;
                }
                catch (Exception ex)
                {
                }
                ++index;
            }
        }
    }
}
