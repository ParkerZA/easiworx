// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Validation.TaxNumber
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System.Text.RegularExpressions;

namespace my.domain.lib.core.Validation
{
    internal class TaxNumber
    {
        private string _TaxNumber = string.Empty;
        private static Regex _expression;
        private Match _match;
        private const string _IDExpression = "(?<Year>[0-9][0-9])(?<Month>([0][1-9])|([1][0-2]))(?<Day>([0-2][0-9])|([3][0-1]))(?<Gender>[0-9])(?<Series>[0-9]{3})(?<Citizenship>[0-9])(?<Uniform>[0-9])(?<Control>[0-9])";

        public TaxNumber(string TaxNumber)
        {
            this._TaxNumber = TaxNumber;
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
                if (F.IsNumeric((object)this._TaxNumber) && this._TaxNumber.Length == 10 && this._TaxNumber != "0000000000")
                {
                    int num1 = int.Parse(this._TaxNumber.Substring(0, 1)) * 2;
                    if (num1 > 9)
                        num1 = int.Parse(num1.ToString().Substring(0, 1)) + int.Parse(num1.ToString().Substring(1, 1));
                    int num2 = int.Parse(this._TaxNumber.Substring(2, 1)) * 2;
                    if (num2 > 9)
                        num2 = int.Parse(num2.ToString().Substring(0, 1)) + int.Parse(num2.ToString().Substring(1, 1));
                    int num3 = int.Parse(this._TaxNumber.Substring(4, 1)) * 2;
                    if (num3 > 9)
                        num3 = int.Parse(num3.ToString().Substring(0, 1)) + int.Parse(num3.ToString().Substring(1, 1));
                    int num4 = int.Parse(this._TaxNumber.Substring(6, 1)) * 2;
                    if (num4 > 9)
                        num4 = int.Parse(num4.ToString().Substring(0, 1)) + int.Parse(num4.ToString().Substring(1, 1));
                    int num5 = int.Parse(this._TaxNumber.Substring(8, 1)) * 2;
                    if (num5 > 9)
                        num5 = int.Parse(num5.ToString().Substring(0, 1)) + int.Parse(num5.ToString().Substring(1, 1));
                    string str = (num1 + num2 + num3 + num4 + num5 + (int.Parse(this._TaxNumber.Substring(1, 1)) + int.Parse(this._TaxNumber.Substring(3, 1)) + int.Parse(this._TaxNumber.Substring(5, 1)) + int.Parse(this._TaxNumber.Substring(7, 1)) + int.Parse(this._TaxNumber.Substring(9, 1)))).ToString();
                    if (str.Substring(str.Length - 1, 1) == "0")
                        return true;
                }
                return false;
            }
        }
    }
}
