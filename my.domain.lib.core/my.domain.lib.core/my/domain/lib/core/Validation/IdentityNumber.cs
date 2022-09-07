// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Validation.IdentityNumber
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.Text.RegularExpressions;

namespace my.domain.lib.core.Validation
{
    internal class IdentityNumber
    {
        private static Regex _expression = new Regex("(?<Year>[0-9][0-9])(?<Month>([0][1-9])|([1][0-2]))(?<Day>([0-2][0-9])|([3][0-1]))(?<Gender>[0-9])(?<Series>[0-9]{3})(?<Citizenship>[0-9])(?<Uniform>[0-9])(?<Control>[0-9])", RegexOptions.Compiled | RegexOptions.Singleline);
        private Match _match;
        private const string _IDExpression = "(?<Year>[0-9][0-9])(?<Month>([0][1-9])|([1][0-2]))(?<Day>([0-2][0-9])|([3][0-1]))(?<Gender>[0-9])(?<Series>[0-9]{3})(?<Citizenship>[0-9])(?<Uniform>[0-9])(?<Control>[0-9])";

        public IdentityNumber(string IDNumber)
        {
            this._match = IdentityNumber._expression.Match(IDNumber.Trim());
        }

        public string DateOfBirth
        {
            get
            {
                if (!this.IsUsable)
                    throw new ArgumentException("ID Number is unusable!", "IDNumber");
                int num1 = int.Parse(this._match.Groups["Year"].Value);
                int num2 = int.Parse(DateTime.Now.Year.ToString().Substring(0, 2) + "00");
                int num3 = num2 - 100;
                int num4 = int.Parse(DateTime.Now.Year.ToString().Substring(2, 2));
                return string.Format("{0}{1}{2}", (object)(num1 <= num4 ? num1 + num2 : num1 + num3), (object)this._match.Groups["Month"].Value, (object)this._match.Groups["Day"].Value);
            }
        }

        public IdentityNumber.PersonGender Gender
        {
            get
            {
                if (!this.IsUsable)
                    throw new ArgumentException("ID Number is unusable!", "IDNumber");
                return int.Parse(this._match.Groups[nameof(Gender)].Value) < 5 ? IdentityNumber.PersonGender.Female : IdentityNumber.PersonGender.Male;
            }
        }

        public IdentityNumber.PersonCitizenship Citizenship
        {
            get
            {
                if (!this.IsUsable)
                    throw new ArgumentException("ID Number is unusable!", "IDNumber");
                return (IdentityNumber.PersonCitizenship)Enum.Parse(typeof(IdentityNumber.PersonCitizenship), this._match.Groups[nameof(Citizenship)].Value);
            }
        }

        public bool IsUsable
        {
            get
            {
                return this._match.Success;
            }
        }

        public bool IsValid
        {
            get
            {
                if (this.IsUsable && this._match.Value.Substring(6) != "0000000")
                {
                    int num1 = int.Parse(this._match.Value.Substring(0, 1)) + int.Parse(this._match.Value.Substring(2, 1)) + int.Parse(this._match.Value.Substring(4, 1)) + int.Parse(this._match.Value.Substring(6, 1)) + int.Parse(this._match.Value.Substring(8, 1)) + int.Parse(this._match.Value.Substring(10, 1));
                    int num2 = int.Parse(this._match.Value.Substring(1, 1) + this._match.Value.Substring(3, 1) + this._match.Value.Substring(5, 1) + this._match.Value.Substring(7, 1) + this._match.Value.Substring(9, 1) + this._match.Value.Substring(11, 1)) * 2;
                    string str1 = num2.ToString();
                    num2 = 0;
                    for (int startIndex = 0; startIndex < str1.Length; ++startIndex)
                        num2 += int.Parse(str1.Substring(startIndex, 1));
                    string str2 = (num1 + num2).ToString();
                    string str3 = str2.Substring(str2.Length - 1, 1);
                    int num3 = 0;
                    if (str3 != "0")
                        num3 = 10 - int.Parse(str3.Substring(str3.Length - 1, 1));
                    if (this._match.Groups["Control"].Value == num3.ToString())
                        return true;
                }
                return false;
            }
        }

        public enum PersonGender
        {
            Female = 0,
            Male = 5,
        }

        public enum PersonCitizenship
        {
            SouthAfrican,
            Foreign,
        }
    }
}
