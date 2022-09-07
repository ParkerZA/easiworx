// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Registry.RegistryWrapper
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using Microsoft.Win32;
using System;

namespace my.domain.lib.core.Registry
{
    public static class RegistryWrapper
    {
        public static string ReadRegistry(string AppKey, string Key, string Default = null)
        {
            try
            {
                RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(AppKey, false);
                if (registryKey == null)
                    throw new Exception("Registry Key Invalid or not defined");
                return (string)registryKey.GetValue(Key, (object)Default);
            }
            catch (Exception ex)
            {
            }
            return Default;
        }

        public static void WriteRegistry(string AppKey, string Key, object Value)
        {
            RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(AppKey, true) ?? Microsoft.Win32.Registry.CurrentUser.CreateSubKey(AppKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
            if (Value == null)
                Value = (object)string.Empty;
            registryKey.SetValue(Key, Value, RegistryValueKind.String);
        }
    }
}
