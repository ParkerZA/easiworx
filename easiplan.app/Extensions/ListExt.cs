using Finx.App;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easiplan.app.Extensions
    {
        public static class ListExt
        {
            public static List<ListDataItem> ToListDataItem<T>(this IList<T> list, string text, string value)
            {
                List<ListDataItem> _list = new List<ListDataItem>();

                foreach (T l in list)
                {
                    var text_ = l.GetType().GetProperty(text).GetValue(l).ToString();
                    var value_ = l.GetType().GetProperty(value).GetValue(l);

                    _list.Add(new ListDataItem() { Text = text_, Value = value_ });
                }

                return _list;
            }
            public static List<ListDataItem> ToListDataItem<T>(this IEnumerable<T> list, string text, string value)
            {
                List<ListDataItem> _list = new List<ListDataItem>();

                foreach (T l in list)
                {
                    var text_ = l.GetType().GetProperty(text).GetValue(l).ToString();
                    var value_ = l.GetType().GetProperty(value).GetValue(l);

                    _list.Add(new ListDataItem() { Text = text_, Value = value_ });
                }

                return _list;
            }

            public static IList<T> ToList<T>(this IBindingList bindList)
            {
                IList<T> list = new List<T>();
                try
                {
                    foreach (var item in bindList)
                        list.Add((T)item);
                }
                catch (Exception x) { }

                return list;
            }

        }
    }
