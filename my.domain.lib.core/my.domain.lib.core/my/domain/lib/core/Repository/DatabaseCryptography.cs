// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Repository.DatabaseCryptography
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using my.domain.lib.core.Attributes;
using my.domain.lib.core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace my.domain.lib.core.Repository
{
    internal class DatabaseCryptography
    {
        internal void EncryptProperties(object entity, string[] propertyNames, object[] state)
        {
            this.Hash(entity, propertyNames, state);
            this.Crypt(entity, propertyNames, state, true);
        }

        private void Crypt(object entity, string[] propertyNames, object[] state, bool Encrypt = true)
        {
            if (entity == null)
                return;
            foreach (PropertyInfo property in entity.GetType().GetProperties())
            {
                if (((IEnumerable<object>)property.GetCustomAttributes(typeof(EncryptAttribute), true)).Any<object>())
                {
                    string name = property.Name;
                    int index = 0;
                    foreach (string propertyName in propertyNames)
                    {
                        if (string.Equals(propertyName, name, StringComparison.InvariantCultureIgnoreCase))
                        {
                            string str1 = Convert.ToString(state[index]);
                            if (!string.IsNullOrEmpty(str1))
                            {
                                if (Encrypt)
                                {
                                    string str2 = str1.Encrypt("key1");
                                    state[index] = (object)str2;
                                }
                                else
                                {
                                    string str2 = str1.Decrypt("key1");
                                    state[index] = str2.ToType(property.PropertyType);
                                }
                                break;
                            }
                            break;
                        }
                        ++index;
                    }
                }
            }
        }

        private void Hash(object entity, string[] propertyNames, object[] state)
        {
            if (entity == null)
                return;
            foreach (PropertyInfo property in entity.GetType().GetProperties())
            {
                if (((IEnumerable<object>)property.GetCustomAttributes(typeof(HashAttribute), true)).Any<object>())
                {
                    string name = property.Name;
                    int index = 0;
                    foreach (string propertyName in propertyNames)
                    {
                        if (string.Equals(propertyName, name, StringComparison.InvariantCultureIgnoreCase))
                        {
                            object obj = state[index];
                            object hash = (object)entity.computeHash();
                            state[index] = hash;
                            break;
                        }
                        ++index;
                    }
                }
            }
        }

        internal void DecryptProperties(object entity, string[] propertyNames, object[] state)
        {
            this.Crypt(entity, propertyNames, state, false);
        }
    }
}
