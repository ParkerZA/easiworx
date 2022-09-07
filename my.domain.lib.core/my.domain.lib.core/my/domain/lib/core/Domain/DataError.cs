// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.DataError
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

namespace my.domain.lib.core.Domain
{
    public class DataError
    {
        public DataErrorDescEnum DataErrorDescEnum { get; set; }

        public string ControlReference { get; set; }

        public string[] Parameters { get; set; }

        public DataError(
          DataErrorDescEnum dataErrorDescEnum,
          string controlReference = "",
          params string[] parameters)
        {
            this.DataErrorDescEnum = dataErrorDescEnum;
            this.ControlReference = controlReference;
            this.Parameters = parameters;
        }
    }
}
