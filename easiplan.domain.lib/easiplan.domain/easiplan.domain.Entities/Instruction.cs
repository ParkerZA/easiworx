using my.domain.lib.core.Attributes;
using System;

namespace easiplan.domain.Entities
{
	public class Instruction : BaseEntity<int>
	{
		private string _Type;

		private string _Description;

		private string _Comment;

		private string _AllocatedTo;

		private int _ReferenceId;

		private string _ReferenceNo;

		private int _ClientId;

		private InstructionType _InstructionType;

        private string _TaskName;

        [NHMaxLengthAttr(4000)]
		public virtual string Type
		{
			get
			{
				return _Type;
			}
			set
			{
				_Type = value;
				InvokePropertyChanged("Type");
			}
		}

		public virtual string Description
		{
			get
			{
				return _Description;
			}
			set
			{
				_Description = value;
				InvokePropertyChanged("Description");
			}
		}

		[NHMaxLengthAttr(4000)]
		public virtual string Comment
		{
			get
			{
				return _Comment;
			}
			set
			{
				_Comment = value;
				InvokePropertyChanged("Comment");
			}
		}

		public virtual string AllocatedTo
		{
			get
			{
				return _AllocatedTo;
			}
			set
			{
                if (!value.Equals(_AllocatedTo))
                {
                    _AllocatedTo = value;
                    InvokePropertyChanged("AllocatedTo");
                }
			}
		}

		public virtual int ReferenceId
		{
			get
			{
				return _ReferenceId;
			}
			set
			{
				_ReferenceId = value;
				InvokePropertyChanged("ReferenceId");
			}
		}

		public virtual string ReferenceNo
		{
			get
			{
				return _ReferenceNo;
			}
			set
			{
				_ReferenceNo = value;
				InvokePropertyChanged("ReferenceNo");
			}
		}

        public virtual string ReferenceOwner
        {
            get; set;
        }

        public virtual int ClientId
		{
			get
			{
				return _ClientId;
			}
			set
			{
				_ClientId = value;
				InvokePropertyChanged("ClientId");
			}
		}

		public virtual InstructionType InstructionType
		{
			get
			{
				return _InstructionType;
			}
			set
			{
				_InstructionType = value;
				InvokePropertyChanged("InstructionType");
			}
		}

        //[IgnoreAutoMap]
        public virtual string TaskName
        {
            get
            {
                return _TaskName;
            }
            set
            {
				_TaskName = value;
				InvokePropertyChanged("TaskName");
			}
        }


        int _daysLastUpdated = 0;
        [IgnoreAutoMap]
        public virtual int DaysLastUpdated
		{
            get { Calculate(); return _daysLastUpdated; }
            set { }
		}

        int _daysCreated = 0;
        [IgnoreAutoMap]
		public virtual int DaysCreated
		{
            get { Calculate(); return _daysCreated; }
            set { }
        }

		public Instruction()
		{
			AllocatedTo = "UnAllocated";
		}

		public override void Calculate()
		{
			if (!InvokeModelCalculating())
				return;

			DateTime now = DateTime.Now;
			TimeSpan timeSpan = now.Subtract(UpdateDate);
            _daysLastUpdated = (int)timeSpan.TotalDays;
			now = DateTime.Now;
			timeSpan = now.Subtract(CreateDate);
            _daysCreated = (int)timeSpan.TotalDays;

			InvokeModelCalculated();
		}
	}
}
