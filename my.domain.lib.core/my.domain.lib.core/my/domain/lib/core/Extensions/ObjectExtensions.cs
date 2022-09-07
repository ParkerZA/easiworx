// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Extensions.ObjectExtensions
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace my.domain.lib.core.Extensions
{
    public static class ObjectExtensions
    {
        public static IEnumerable<T> ListClassAttributes<T>(this object source)
        {
            List<T> objList = new List<T>();
            if (source == null)
                return (IEnumerable<T>)objList;
            foreach (T customAttribute in source.GetType().GetCustomAttributes(typeof(T), false))
                objList.Add(customAttribute);
            return (IEnumerable<T>)objList;
        }

        public static IEnumerable<T> ListMethodAttributes<T>(this MethodInfo source)
        {
            List<T> objList = new List<T>();
            if (source == (MethodInfo)null)
                return (IEnumerable<T>)objList;
            foreach (System.Attribute customAttribute in System.Attribute.GetCustomAttributes((MemberInfo)source, true))
            {
                if (customAttribute.GetType() == typeof(T))
                    objList.Add((T)customAttribute.ToType(typeof(T)));
            }
            return (IEnumerable<T>)objList;
        }

        public static IEnumerable<T> ListAttributes<T>(this object source)
        {
            List<T> objList = new List<T>();
            if (source == null)
                return (IEnumerable<T>)objList;
            foreach (MemberInfo property in source.GetType().GetProperties())
            {
                foreach (T customAttribute in property.GetCustomAttributes(typeof(T), false))
                    objList.Add(customAttribute);
            }
            return (IEnumerable<T>)objList;
        }

        public static MethodInfo GetMethodInfo(this object source, string MethodName)
        {
            Type type = source.GetType();
            try
            {
                return type.GetMethod(MethodName);
            }
            catch (Exception ex)
            {
            }
            return (MethodInfo)null;
        }

        public static object GetPropertyValue(this object obj, string PropertyName)
        {
            PropertyInfo propertyInfo = ((IEnumerable<PropertyInfo>)obj.GetType().GetProperties()).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(x => x.Name == PropertyName));
            if (!(propertyInfo != (PropertyInfo)null))
                return (object)null;
            object obj1 = propertyInfo.GetValue(obj, (object[])null);
            if (propertyInfo.PropertyType == typeof(DateTime))
                return (DateTime)obj1 == DateTime.MinValue ? (object)"" : obj1;
            return obj1;
        }

        public static Type GetItemTypeFromCollection(object collection)
        {
            Type type = collection.GetType().GetInterface(typeof(IEnumerable<>).Name);
            return type != (Type)null ? type.GetGenericArguments()[0] : (Type)null;
        }

        public static object ToType(this object obj, Type type)
        {
            try
            {
                return Convert.ChangeType(obj, type);
            }
            catch (Exception ex)
            {
                if ((uint)type.GenericTypeArguments.Length > 0U)
                    obj = Convert.ChangeType(obj, type.GenericTypeArguments[0]);
            }
            return obj;
        }

        public static T ToObject<T>(this object obj) where T : new()
        {
            try
            {
                Type type = obj.GetType();
                List<KeyValuePair<string, string>> columnMapping = new List<KeyValuePair<string, string>>();
                foreach (PropertyInfo property in type.GetProperties())
                    columnMapping.Add(new KeyValuePair<string, string>(property.Name, property.Name));
                return obj.ToObject<T>(columnMapping);
            }
            catch (Exception ex)
            {
            }
            return default(T);
        }

        public static T ToObject<T>(this object obj, List<KeyValuePair<string, string>> columnMapping) where T : new()
        {
            T obj1 = new T();
            try
            {
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    PropertyInfo objProperty = property;
                    try
                    {
                        string PropertyName = columnMapping.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>)(x => x.Key.ToLower() == objProperty.Name.ToLower())).First<KeyValuePair<string, string>>().Value.ToString();
                        if (!string.IsNullOrEmpty(PropertyName))
                        {
                            string str1 = obj.GetPropertyValue(PropertyName).ToString();
                            if (!string.IsNullOrEmpty(str1))
                            {
                                if (Nullable.GetUnderlyingType(objProperty.PropertyType) != (Type)null)
                                {
                                    string str2 = str1.Replace("$", "").Replace(",", "");
                                    objProperty.SetValue((object)obj1, Convert.ChangeType((object)str2, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), (object[])null);
                                }
                                else
                                {
                                    string str2 = str1.Replace("%", "");
                                    objProperty.SetValue((object)obj1, Convert.ChangeType((object)str2, Type.GetType(objProperty.PropertyType.ToString())), (object[])null);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return obj1;
        }

        public static IList<T> ToListObject<U, T>(
          this IList<U> obj,
          List<KeyValuePair<string, string>> columnMapping)
          where T : new()
        {
            IList<T> objList = (IList<T>)new List<T>();
            try
            {
                foreach (U u in (IEnumerable<U>)obj)
                    objList.Add(((object)u).ToObject<T>(columnMapping));
                return objList;
            }
            catch (Exception ex)
            {
            }
            return objList;
        }

        public static IList<T> ToListObject<U, T>(this IList<U> obj) where T : new()
        {
            IList<T> result = (IList<T>)new List<T>();
            try
            {
                Type type = typeof(U);
                List<KeyValuePair<string, string>> columnMapping = new List<KeyValuePair<string, string>>();
                foreach (PropertyInfo property in type.GetProperties())
                    columnMapping.Add(new KeyValuePair<string, string>(property.Name, property.Name));
                Parallel.ForEach<U>((IEnumerable<U>)obj, (Action<U>)(x => result.Add(((object)x).ToObject<T>(columnMapping))));
                return result;
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public static T CopyTo<T>(this object obj, T obj2) where T : new()
        {
            try
            {
                Type type = obj.GetType();
                List<KeyValuePair<string, string>> columnMapping = new List<KeyValuePair<string, string>>();
                foreach (PropertyInfo property in type.GetProperties())
                    columnMapping.Add(new KeyValuePair<string, string>(property.Name, property.Name));
                return obj.CopyTo<T>(obj2, columnMapping);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public static T CopyTo<T>(
          this object obj,
          T obj2,
          List<KeyValuePair<string, string>> columnMapping)
          where T : new()
        {
            try
            {
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    PropertyInfo objProperty = property;
                    try
                    {
                        string PropertyName = columnMapping.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>)(x => x.Key.ToLower() == objProperty.Name.ToLower())).First<KeyValuePair<string, string>>().Value.ToString();
                        if (!string.IsNullOrEmpty(PropertyName))
                        {
                            string str1 = obj.GetPropertyValue(PropertyName).ToString();
                            if (!string.IsNullOrEmpty(str1))
                            {
                                if (Nullable.GetUnderlyingType(objProperty.PropertyType) != (Type)null)
                                {
                                    string str2 = str1.Replace("$", "").Replace(",", "");
                                    objProperty.SetValue((object)obj2, Convert.ChangeType((object)str2, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), (object[])null);
                                }
                                else
                                {
                                    string str2 = str1.Replace("%", "");
                                    objProperty.SetValue((object)obj2, Convert.ChangeType((object)str2, Type.GetType(objProperty.PropertyType.ToString())), (object[])null);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                return obj2;
            }
            catch (Exception ex)
            {
                return obj2;
            }
        }

        public static int ToInt(this object obj)
        {
            try
            {
                return int.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
            }
            return -1;
        }

        public static Dictionary<string, object> ToPropertyDictionary(this object obj)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                if (property.CanRead && property.GetIndexParameters().Length == 0)
                    dictionary[property.Name] = property.GetValue(obj, (object[])null);
            }
            return dictionary;
        }

        public static byte[] computeHash(this object instance)
        {
            MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                new DataContractSerializer(instance.GetType()).WriteObject((Stream)memoryStream, instance);
                cryptoServiceProvider.ComputeHash(memoryStream.ToArray());
                return cryptoServiceProvider.Hash;
            }
        }

        public static XmlDocument ToXml<T>(this object model)
        {
            XmlDocument xmlDocument = new XmlDocument();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (MyXmlTextWriter myXmlTextWriter = new MyXmlTextWriter((Stream)memoryStream))
                {
                    new DataContractSerializer(typeof(T)).WriteObject((XmlWriter)myXmlTextWriter, model);
                    myXmlTextWriter.Flush();
                    memoryStream.Seek(0L, SeekOrigin.Begin);
                    xmlDocument.Load((Stream)memoryStream);
                }
            }
            return xmlDocument;
        }

        public static XmlDocument Serialize<T>(this object model)
        {
            XmlDocument xmlDocument = new XmlDocument();
            DataContractSerializer contractSerializer = new DataContractSerializer(typeof(T));
            StringBuilder sb = new StringBuilder();
            using (StringWriter stringWriter1 = new StringWriter(sb))
            {
                StringWriter stringWriter2 = stringWriter1;
                using (XmlWriter writer = XmlWriter.Create((TextWriter)stringWriter2, new XmlWriterSettings()
                {
                    Indent = true,
                    Encoding = Encoding.UTF8,
                    OmitXmlDeclaration = true,
                    NamespaceHandling = NamespaceHandling.OmitDuplicates
                }))
                    contractSerializer.WriteObject(writer, model);
            }
            xmlDocument.LoadXml(sb.ToString());
            return xmlDocument;
        }

        public static T Deserialize<T>(this object model, XmlDocument XmlDocument)
        {
            using (StringReader stringReader = new StringReader(XmlDocument.OuterXml))
            {
                using (XmlReader reader = XmlReader.Create((TextReader)stringReader))
                    return (T)new DataContractSerializer(typeof(T)).ReadObject(reader);
            }
        }
    }
}
