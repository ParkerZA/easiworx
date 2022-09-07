// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.DataResult
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System.Collections.Generic;

namespace my.domain.lib.core.Domain
{
    public class DataResult
    {
        private static readonly DataResult _success = new DataResult(true);

        public DataResult(params DataError[] errors)
          : this((IEnumerable<DataError>)errors)
        {
        }

        public DataResult(IEnumerable<DataError> errors)
        {
            if (errors == null)
                errors = (IEnumerable<DataError>)new DataError[1]
                {
          new DataError(DataErrorDescEnum.Unknown, "", new string[0])
                };
            this.Succeeded = false;
            this.Errors = errors;
        }

        private DataResult(bool success)
        {
            this.Succeeded = success;
            this.Errors = (IEnumerable<DataError>)new DataError[0];
        }

        public bool Succeeded { get; private set; }

        public IEnumerable<DataError> Errors { get; private set; }

        public static DataResult Success
        {
            get
            {
                return DataResult._success;
            }
        }

        public static DataResult Failed(params DataError[] errors)
        {
            return new DataResult(errors);
        }
    }
}
