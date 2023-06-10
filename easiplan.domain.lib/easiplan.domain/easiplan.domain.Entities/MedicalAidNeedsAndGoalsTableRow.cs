using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easiplan.domain.Entities
{

    public class MedicalAidNeedsAndGoalsTableRow : BaseEntity<int>
    {
        private string _coverDiscussed;
        private string _coverTaken;
        private string _coverComment;

        public virtual string CoverDiscussed
        {
            get
            {
                return _coverDiscussed;
            }
            set
            {
                _coverDiscussed = value;
                InvokePropertyChanged("CoverDiscussed");
            }
        }

        public virtual string CoverTaken
        {
            get
            {
                return _coverTaken;
            }
            set
            {
                _coverTaken = value;
                InvokePropertyChanged("CoverTaken");
            }
        }

        public virtual string Comments
        {
            get
            {
                return _coverComment;
            }
            set
            {
                _coverComment = value;
                InvokePropertyChanged("Comments");
            }
        }

    }
}
