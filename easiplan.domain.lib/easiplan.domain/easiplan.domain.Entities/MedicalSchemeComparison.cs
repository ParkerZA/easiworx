using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easiplan.domain.easiplan.domain.Entities
{
    public class MedicalSchemeComparison : BaseEntity<int>
    {
        private string _currentMedicalScheme;
        private string _replacedMedicalScheme;

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
