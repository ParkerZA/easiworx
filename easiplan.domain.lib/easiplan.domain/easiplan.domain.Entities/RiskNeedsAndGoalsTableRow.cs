using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easiplan.domain.Entities
{
    public class RiskNeedsAndGoalsTableRow : BaseEntity<int>
    {
        private string _needsQuantified;
        private string _needsPriority;
        private string _needAddressed;
        private string _shortfall;
        private string _reviewDate;

        public RiskNeedsAndGoalsTableRow() 
        {
            this._needsQuantified = string.Empty;
            this._needsPriority = string.Empty;
            this._needAddressed = string.Empty;
            this._shortfall = string.Empty;
            this._reviewDate = string.Empty;
        }

        public RiskNeedsAndGoalsTableRow( RiskNeedsAndGoalsTableRow copy)
        {
            this._needsQuantified= copy._needsQuantified;
            this._needsPriority= copy._needsPriority;
            this._needAddressed= copy._needAddressed;
            this._shortfall= copy._shortfall;
            this._reviewDate= copy._reviewDate;
        }


        public virtual string NeedsQuantified
        {
            get
            {
                {
                    return _needsQuantified;
                }
            }
            set
            {
                _needsQuantified = value.Replace("R", string.Empty).Trim();
                InvokePropertyChanged("NeedsQuantified");
            }
        }

        public virtual string NeedsPriority
        {
            get
            {
                return _needsPriority;
            }
            set
            {
                _needsPriority = value;
                InvokePropertyChanged("NeedsPriority");
            }
        }

        public virtual string NeedAddressed
        {
            get
            {
                return _needAddressed;
            }
            set
            {
                _needAddressed = value;
                InvokePropertyChanged("NeedAddressed");
            }
        }

        public virtual string Shortfall
        {
            get
            {
                return _shortfall;
            }
            set
            {
                _shortfall = value;
                InvokePropertyChanged("Shortfall");
            }
        }

        public virtual string ReviewDate
        {
            get
            {
                return _reviewDate;
            }
            set
            {
                _reviewDate = value;
                InvokePropertyChanged("ReviewDate");
            }
        }


    }
}
