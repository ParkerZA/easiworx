// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.MySqlDataDriverLogger
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using NHibernate.AdoNet;
using NHibernate.Driver;
using System;
using System.Collections;
using System.Data;

namespace my.domain.lib.core.Repository
{
    public class MySqlDataDriverLogger : MySqlDataDriver, IEmbeddedBatcherFactoryProvider
    {
        public override void AdjustCommand(IDbCommand command)
        {
            string empty = string.Empty;
            try
            {
                string str = command.CommandText.ToString();
                foreach (IDataParameter parameter in (IEnumerable)command.Parameters)
                {
                    switch (parameter.DbType)
                    {
                        case DbType.Decimal:
                        case DbType.Double:
                        case DbType.Int16:
                        case DbType.Int32:
                        case DbType.Int64:
                            str = str.Replace(string.Format("{0}", (object)parameter.ParameterName), string.Format("{0}", (object)parameter.Value.ToString()));
                            break;
                        default:
                            str = str.Replace(string.Format("{0}", (object)parameter.ParameterName), string.Format("'{0}'", (object)parameter.Value.ToString()));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            base.AdjustCommand(command);
        }

        public Type BatcherFactoryClass
        {
            get
            {
                return (Type)null;
            }
        }
    }
}
