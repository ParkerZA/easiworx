// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Validation.SaIdValidator
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;

namespace my.domain.lib.core.Validation
{
    public class SaIdValidator
    {
        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string Nationality { get; set; }

        public bool Validate(string IdentificationNo)
        {
            try
            {
                try
                {
                    if (IdentificationNo.Length != 13 || !F.IsNumeric((object)IdentificationNo))
                        return false;
                    IdentityNumber identityNumber = new IdentityNumber(IdentificationNo);
                    if (identityNumber.IsUsable && identityNumber.IsValid)
                    {
                        this.DateOfBirth = DateTime.Parse(string.Format("{0}/{1}/20{2}", (object)IdentificationNo.Substring(4, 2), (object)IdentificationNo.Substring(2, 2), (object)IdentificationNo.Substring(0, 2)));
                        if (this.DateOfBirth.CompareTo(DateTime.Now) > 0)
                            this.DateOfBirth = this.DateOfBirth.AddYears(-100);
                        switch (identityNumber.Gender)
                        {
                            case IdentityNumber.PersonGender.Female:
                                this.Gender = "Female";
                                break;
                            case IdentityNumber.PersonGender.Male:
                                this.Gender = "Male";
                                break;
                        }
                        this.Nationality = identityNumber.Citizenship != IdentityNumber.PersonCitizenship.SouthAfrican ? "Foreign" : "South African";
                        return true;
                    }
                }
                catch (Exception ex)
                {
                }
                return false;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
    }
}
