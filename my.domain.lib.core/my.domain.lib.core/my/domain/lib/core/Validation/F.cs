// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Validation.F
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace my.domain.lib.core.Validation
{
    public static class F
    {
        public static double Sin(double v)
        {
            return Math.Sin(v);
        }

        public static double Cos(double v)
        {
            return Math.Cos(v);
        }

        public static double Mod(double x, double y)
        {
            return x % y;
        }

        public static double Exp(double Base, double pexp)
        {
            return Math.Pow(Base, pexp);
        }

        public static double Sqrt(double v)
        {
            return Math.Sqrt(v);
        }

        public static double Power(double v, double e)
        {
            return Math.Pow(v, e);
        }

        public static int SumOf(params int[] args)
        {
            int num1 = 0;
            foreach (int num2 in args)
                num1 += num2;
            return num1;
        }

        public static double SumOf(params double[] args)
        {
            double num1 = 0.0;
            foreach (double num2 in args)
                num1 += num2;
            return num1;
        }

        public static DateTime Now()
        {
            return DateTime.Now;
        }

        public static DateTime Date(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }

        public static int Year(DateTime d)
        {
            return d.Year;
        }

        public static int Month(DateTime d)
        {
            return d.Month;
        }

        public static int Day(DateTime d)
        {
            return d.Day;
        }

        public static DateTime DateAdd(object obj, double days)
        {
            DateTime dateTime = DateTime.Parse(obj.ToString());
            try
            {
                return dateTime.AddDays(days);
            }
            catch (Exception ex)
            {
            }
            return DateTime.MinValue;
        }

        public static DateTime DateAddMonths(object obj, int month)
        {
            DateTime dateTime = DateTime.Parse(obj.ToString());
            try
            {
                return dateTime.AddMonths(month);
            }
            catch (Exception ex)
            {
            }
            return DateTime.MinValue;
        }

        public static int DateDiff(object obj, object obj2)
        {
            try
            {
                return DateTime.Parse(obj.ToString()).Subtract(DateTime.Parse(obj2.ToString())).Days;
            }
            catch (Exception ex)
            {
            }
            return 0;
        }

        public static string Trim(string str)
        {
            return str.Trim();
        }

        public static string LeftTrim(string str)
        {
            return str.TrimStart();
        }

        public static string RightTrim(string str)
        {
            return str.TrimEnd();
        }

        public static string PadLeft(string str, int wantedlen, string addedchar)
        {
            while (str.Length < wantedlen)
                str = addedchar + str;
            return str;
        }

        public static string Lower(string value)
        {
            return value.ToLower();
        }

        public static string Upper(string value)
        {
            return value.ToUpper();
        }

        public static string WCase(string value)
        {
            if (value.Length == 0)
                return "";
            return value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower();
        }

        public static string Replace(string Base, string search, string repl)
        {
            return Base.Replace(search, repl);
        }

        public static string Substr(string s, int from, int len)
        {
            if (s == null)
                return string.Empty;
            --from;
            if (from < 1)
                from = 0;
            if (from >= s.Length)
                from = s.Length;
            if (from + len > s.Length)
                len = s.Length - from;
            return s.Substring(from, len);
        }

        public static int Len(string str)
        {
            if (str == null)
                return 0;
            return str.Length;
        }

        public static string[] Split(string s, string delimiter)
        {
            return s.Split(delimiter[0]);
        }

        public static object If(bool cond, object TrueValue, object FalseValue)
        {
            if (cond)
                return TrueValue;
            return FalseValue;
        }

        public static object IIF(bool Expression, bool Expression2)
        {
            if (Expression)
                return (object)Expression2;
            return (object)true;
        }

        public static double Abs(double val)
        {
            if (val < 0.0)
                return val * -1.0;
            return val;
        }

        public static int Int(object value)
        {
            return (int)value;
        }

        public static int Trunc(double value, int prec)
        {
            value -= 0.5 / Math.Pow(10.0, (double)prec);
            return (int)Math.Round(value, prec);
        }

        public static double Dec(object value)
        {
            return (double)value;
        }

        public static double Round(object value)
        {
            return Math.Round((double)value);
        }

        public static string Chr(int c)
        {
            return ((char)c).ToString() ?? "";
        }

        public static string ChCR()
        {
            return "\r";
        }

        public static string ChLF()
        {
            return "\n";
        }

        public static string ChCRLF()
        {
            return "\r\n";
        }

        public static bool IsRequired(object obj)
        {
            if (obj == null)
                return false;
            string name = obj.GetType().Name;
            if (!(name == "DateTime"))
            {
                if (name == "Integer")
                {
                    if ((int)obj > int.Parse("0".ToString()))
                        return true;
                }
                else if (!string.IsNullOrEmpty(obj.ToString()))
                    return true;
            }
            else if (DateTime.Compare((DateTime)obj, DateTime.Parse(DateTime.MinValue.ToString())) > 0)
                return true;
            return false;
        }

        public static bool IsNullOrEmpty(object obj)
        {
            return !F.IsRequired(obj);
        }

        public static bool IsNotNullOrEmpty(object obj)
        {
            return F.IsRequired(obj);
        }

        public static bool IsEqual(object obj, object obj2)
        {
            if (obj == null)
                return false;
            return obj.Equals(obj2);
        }

        public static bool IsNotEqual(object obj, object obj2)
        {
            if (obj == null)
                return false;
            return !obj.Equals(obj2);
        }

        public static bool IsGreaterThan(object obj, object obj2)
        {
            if (obj == null)
                return false;
            string name = obj.GetType().Name;
            if (!(name == "DateTime"))
            {
                if (!(name == "Integer"))
                {
                    if (!(name == "Double"))
                    {
                        if (name == "String")
                        {
                            if (!obj.Equals(obj2))
                                return true;
                        }
                        else if (!string.IsNullOrEmpty(obj.ToString()))
                            return true;
                    }
                    else if ((double)obj > double.Parse(obj2.ToString()))
                        return true;
                }
                else if ((int)obj > int.Parse(obj2.ToString()))
                    return true;
            }
            else if (DateTime.Compare((DateTime)obj, DateTime.Parse(obj2.ToString())) > 0)
                return true;
            return false;
        }

        public static bool IsLessThan(object obj, object obj2)
        {
            if (obj == null)
                return false;
            string name = obj.GetType().Name;
            if (!(name == "DateTime"))
            {
                if (!(name == "Integer"))
                {
                    if (!(name == "Double"))
                    {
                        if (name == "String")
                        {
                            if (!obj.Equals(obj2))
                                return true;
                        }
                        else if (!string.IsNullOrEmpty(obj.ToString()))
                            return true;
                    }
                    else if ((double)obj < double.Parse(obj2.ToString()))
                        return true;
                }
                else if ((int)obj < int.Parse(obj2.ToString()))
                    return true;
            }
            else if (DateTime.Compare((DateTime)obj, DateTime.Parse(obj2.ToString())) < 0)
                return true;
            return false;
        }

        public static bool IsGreaterOrEqualThan(object obj, object obj2)
        {
            if (obj == null)
                return false;
            string name = obj.GetType().Name;
            if (!(name == "DateTime"))
            {
                if (!(name == "Integer"))
                {
                    if (!(name == "Double"))
                    {
                        if (name == "String")
                        {
                            if (!obj.Equals(obj2))
                                return true;
                        }
                        else if (!string.IsNullOrEmpty(obj.ToString()))
                            return true;
                    }
                    else if ((double)obj >= double.Parse(obj2.ToString()))
                        return true;
                }
                else if ((int)obj >= int.Parse(obj2.ToString()))
                    return true;
            }
            else if (DateTime.Compare((DateTime)obj, DateTime.Parse(obj2.ToString())) >= 0)
                return true;
            return false;
        }

        public static bool IsLessOrEqualThan(object obj, object obj2)
        {
            if (obj == null)
                return false;
            string name = obj.GetType().Name;
            if (!(name == "DateTime"))
            {
                if (!(name == "Integer"))
                {
                    if (!(name == "Double"))
                    {
                        if (name == "String")
                        {
                            if (!obj.Equals(obj2))
                                return true;
                        }
                        else if (!string.IsNullOrEmpty(obj.ToString()))
                            return true;
                    }
                    else if ((double)obj <= double.Parse(obj2.ToString()))
                        return true;
                }
                else if ((int)obj <= int.Parse(obj2.ToString()))
                    return true;
            }
            else if (DateTime.Compare((DateTime)obj, DateTime.Parse(obj2.ToString())) <= 0)
                return true;
            return false;
        }

        public static bool IsIn(object obj, object obj2)
        {
            if (string.IsNullOrEmpty(obj.ToString()))
                return true;
            if (obj == null || obj2 == null)
                return false;
            return ((IEnumerable<string>)obj2.ToString().ToLower().Split(",".ToCharArray()[0])).Contains<string>(obj.ToString().ToLower());
        }

        public static bool IsLenGreaterThan(object obj, int Len)
        {
            return obj == null || obj.ToString().Length > Len;
        }

        public static bool IsLenEqualGreaterThan(object obj, int Len)
        {
            return obj == null || obj.ToString().Length >= Len;
        }

        public static bool IsLenLessThan(object obj, int Len)
        {
            return obj == null || obj.ToString().Length < Len;
        }

        public static bool IsLenEqualLessThan(object obj, int Len)
        {
            return obj == null || obj.ToString().Length <= Len;
        }

        public static bool IsLenEqual(object obj, int Len)
        {
            return obj == null || obj.ToString().Length == Len;
        }

        public static bool IsSAIdNumber(object obj, object BirthDate, object Gender)
        {
            try
            {
                if (!F.IsNumeric(obj) || obj.ToString().Length != 13)
                    return false;
                DateTime dateTime = DateTime.Parse(BirthDate.ToString());
                IdentityNumber identityNumber = new IdentityNumber((string)obj);
                if (identityNumber.DateOfBirth != string.Format("{0}{1}{2}", (object)dateTime.Year.ToString("00####").Substring(2), (object)dateTime.Month.ToString("0#"), (object)dateTime.Day.ToString("0#")))
                    return false;
                switch (identityNumber.Gender)
                {
                    case IdentityNumber.PersonGender.Female:
                        if (Gender.ToString().ToUpper() != "2" && Gender.ToString().ToUpper() != nameof(F))
                            return false;
                        break;
                    case IdentityNumber.PersonGender.Male:
                        if (Gender.ToString().ToUpper() != "1" && Gender.ToString().ToUpper() != "M")
                            return false;
                        break;
                }
                if (identityNumber.IsUsable && identityNumber.IsValid)
                    return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static bool IsSAIdNumber(object obj)
        {
            try
            {
                if (!F.IsNumeric(obj) || obj.ToString().Length != 13)
                    return false;
                IdentityNumber identityNumber = new IdentityNumber((string)obj);
                if (identityNumber.IsUsable && identityNumber.IsValid)
                    return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static bool IsSATaxNumber(object obj)
        {
            try
            {
                if (new TaxNumber((string)obj).IsValid)
                    return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static bool IsEmail(object email)
        {
            try
            {
                if (email == null)
                    return true;
                string str = email.ToString();
                if (string.IsNullOrEmpty(str))
                    return true;
                return Regex.IsMatch(str.ToLower(), "\\A[a-z0-9]+([-._][a-z0-9]+)*@([a-z0-9]+(-[a-z0-9]+)*\\.)+[a-z]{2,4}\\z") && Regex.IsMatch(str.ToLower(), "^(?=.{1,64}@.{4,64}$)(?=.{6,100}$).*");
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsTelephone(object telephone)
        {
            try
            {
                if (telephone == null)
                    return true;
                string input = telephone.ToString();
                if (string.IsNullOrEmpty(input))
                    return true;
                bool flag = false;
                if (!flag)
                    flag = Regex.IsMatch(input, "^((0|(\\(0\\)))?|(00|(\\(00\\)))?(\\s?|-?)(27|\\(27\\))|((\\+27))|(\\(\\+27\\))|\\(00(\\s?|-?)27\\))( |-)?(\\(?0?\\)?)( |-)?\\(?(1[0-9]|2[1-4,7-9]|3[1-6,9]|4[0-9]|5[1,3,6-9]|7[1-4,6,8,9]|8[0-9])\\)?(\\s?|-?)((\\d{3}(\\s?|-?)\\d{4}$)|((\\d{4})(\\s?|-?)(\\d{3})$)|([0-2](\\s?|-?)(\\d{3}(\\s?|-?)\\d{3}$)))");
                if (!flag)
                    flag = Regex.IsMatch(input, "((?:\\+27|27)|0)(21|72|82|73|83|74|84)(\\d{7})");
                if (!flag)
                    flag = Regex.IsMatch(input, "((?:\\+27|27)|0)( |-)(\\d{3})( |-)(\\d{3})|[0](\\d{2})( |-)(\\d{7})");
                if (!flag)
                    flag = Regex.IsMatch(input, "((?:\\+27|27)|0)(21|72|82|73|83|74|84)(\\d{7})( |-)(\\d{7})");
                return flag;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsCellphone(object cellphone)
        {
            try
            {
                if (cellphone == null)
                    return true;
                string input = cellphone.ToString();
                if (string.IsNullOrEmpty(input))
                    return true;
                bool flag = Regex.IsMatch(input, "(^0[87][23467]((\\d{7})|( |-)((\\d{3}))( |-)(\\d{4})|( |-)(\\d{7})))");
                if (flag)
                    return Regex.IsMatch(input, "^(\\+27|27)?(\\()?0?([7][1-9]|[8][2-4])(\\))?( |-|\\.|_)?(\\d{3})( |-|\\.|_)?(\\d{4})");
                return flag;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsValidAcctNo(object acct_no, string acct_typ, int branch_cd)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static bool IsDate(string date, string format)
        {
            try
            {
                DateTime.ParseExact(date, format, (IFormatProvider)new DateTimeFormatInfo()
                {
                    ShortDatePattern = format
                });
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool IsAlpha(object Value)
        {
            try
            {
                foreach (char c in ((string)Value).Trim())
                {
                    if (!char.IsLetter(c) && c != ' ')
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool IsNumeric(object Value)
        {
            if (Value == null)
                return true;
            try
            {
                foreach (char c in ((string)Value).Trim())
                {
                    if (!char.IsDigit(c) && c != ' ')
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool IsAlphaNumeric(object Value)
        {
            try
            {
                foreach (char c in ((string)Value).Trim())
                {
                    if (!char.IsLetter(c) && !char.IsDigit(c) && c != ' ')
                        throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool IsString(string Expression, int MaxLen)
        {
            return string.IsNullOrEmpty(Expression) || Expression.ToString().Length <= MaxLen;
        }

        public static bool IsDateTime(string Expression, string Format)
        {
            try
            {
                return DateTime.Parse(Expression).Equals(DateTime.MinValue) ? true : true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static bool IsNumeric(string Expression, int MaxLen)
        {
            try
            {
                if (string.IsNullOrEmpty(Expression))
                    return true;
                if (new Regex("^[-+]?[0-9]*\\.?[0-9]+$").IsMatch(Expression.ToString()))
                {
                    if (Expression.ToString().Length <= MaxLen)
                        return true;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static bool IsDecimal(string Expression, int MaxLen, int Precision)
        {
            try
            {
                if (string.IsNullOrEmpty(Expression))
                    return true;
                if (Expression.Length > MaxLen)
                    return false;
                Decimal result;
                Decimal.TryParse(Expression, NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.CurrentUICulture, out result);
                return Expression.Length - (Math.Truncate(result).ToString().Length + 1) <= Precision;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static bool Lookup(string Key, object obj)
        {
            return false;
        }

        public static bool IsAmount(string amount)
        {
            bool flag;
            try
            {
                flag = amount != null && int.Parse(amount) != 0;
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        public static bool IsNullOrWhiteSpace(string value)
        {
            bool flag;
            if (value == null)
            {
                flag = true;
            }
            else
            {
                for (int index = 0; index < value.Length; ++index)
                {
                    if (!char.IsWhiteSpace(value[index]))
                        return false;
                }
                flag = true;
            }
            return flag;
        }
    }
}
