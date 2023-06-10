using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easiplan.domain.Entities
{

    public class MedicalAidNeedsAndGoalsTableRow : BaseEntity<int>
    {
        private string _CoverDiscussed;
        private string _CoverTaken;
        private string _CoverComment;

        public virtual string CoverDiscussed
        {
            get
            {
                return _CoverDiscussed;
            }
            set
            {
                _CoverDiscussed = value;
                InvokePropertyChanged("CoverDiscussed");
            }
        }

        public virtual string CoverTaken
        {
            get
            {
                return _CoverTaken;
            }
            set
            {
                _CoverTaken = value;
                InvokePropertyChanged("CoverTaken");
            }
        }

        public virtual string Comments
        {
            get
            {
                return _CoverComment;
            }
            set
            {
                _CoverComment = value;
                InvokePropertyChanged("Comments");
            }
        }

    }
}
