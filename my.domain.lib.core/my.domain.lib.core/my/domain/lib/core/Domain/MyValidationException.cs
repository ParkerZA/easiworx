// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.MyValidationException
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace my.domain.lib.core.Domain
{
    public class MyValidationException : WarningException
    {
        public MyValidationException()
        {
        }

        public MyValidationException(string message)
          : base(message)
        {
        }

        public MyValidationException(string message, string helpUrl)
          : base(message, helpUrl)
        {
        }

        public MyValidationException(string message, Exception innerException)
          : base(message, innerException)
        {
        }

        public MyValidationException(string message, string helpUrl, string helpTopic)
          : base(message, helpUrl, helpTopic)
        {
        }

        protected MyValidationException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}
