// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.SingleAppMutexControl
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace my.domain.lib.core
{
    public class SingleAppMutexControl : IDisposable
    {
        private readonly Mutex _mutex;
        private readonly bool _hasHandle;

        public SingleAppMutexControl(string appGuid, int waitmillisecondsTimeout = 5000)
        {
            MutexAccessRule rule = new MutexAccessRule((IdentityReference)new SecurityIdentifier(WellKnownSidType.WorldSid, (SecurityIdentifier)null), MutexRights.FullControl, AccessControlType.Allow);
            MutexSecurity mutexSecurity = new MutexSecurity();
            mutexSecurity.AddAccessRule(rule);
            bool createdNew;
            this._mutex = new Mutex(false, "Global\\" + appGuid, out createdNew, mutexSecurity);
            this._hasHandle = false;
            try
            {
                this._hasHandle = this._mutex.WaitOne(waitmillisecondsTimeout, false);
                if (!this._hasHandle)
                    throw new TimeoutException();
            }
            catch (AbandonedMutexException ex)
            {
                this._hasHandle = true;
            }
        }

        public void Dispose()
        {
            if (this._mutex == null)
                return;
            if (this._hasHandle)
                this._mutex.ReleaseMutex();
            this._mutex.Dispose();
        }
    }
}
