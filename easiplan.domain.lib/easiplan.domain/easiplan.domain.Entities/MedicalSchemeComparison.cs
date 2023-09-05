using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easiplan.domain.Entities
{
    public class MedicalSchemeComparison : BaseEntity<int>
    {
        private string _currentMedicalScheme;
        private string _replacedMedicalScheme;

        public MedicalSchemeComparison()
        {
            this._currentMedicalScheme = string.Empty;
            this._replacedMedicalScheme = string.Empty;
        }

        public MedicalSchemeComparison(string name)
        {
            this._currentMedicalScheme = string.Empty;
            this._replacedMedicalScheme = string.Empty;

            this.Name = name;
        }

        public MedicalSchemeComparison(MedicalSchemeComparison copy)
        {
            this._currentMedicalScheme= copy._currentMedicalScheme;
            this._replacedMedicalScheme= copy._replacedMedicalScheme;
        }

        public virtual string CurrentMedicalScheme
        {
            get
            {
                return _currentMedicalScheme;
            }
            set
            {
                _currentMedicalScheme = value;
                InvokePropertyChanged("CurrentMedicalScheme");
            }
        }

        public virtual string ReplacedMedicalScheme
        {
            get
            {
                return _replacedMedicalScheme;
            }
            set
            {
                _replacedMedicalScheme = value;
                InvokePropertyChanged("ReplacedMedicalScheme");
            }
        }
    }
}
