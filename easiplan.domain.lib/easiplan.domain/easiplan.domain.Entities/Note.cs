using System;
using my.domain.lib.core.Attributes;

namespace easiplan.domain.Entities
{
	public class Note : BaseEntity<int>
	{
		private string _Text = "";

		private bool _IsCompleted;

		public virtual string Type
		{
			get;
			set;
		}

		[NHMaxLengthAttr(4000)]
		public virtual string Text
		{
			get
			{
				return _Text;
			}
			set
			{
				_Text = value;
				InvokePropertyChanged("Text");
			}
		}

		public virtual bool IsCompleted
		{
			get
			{
				return _IsCompleted;
			}
			set
			{
				_IsCompleted = value;
				InvokePropertyChanged("IsCompleted");
			}
		}

		public virtual int InstructionId
		{
			get;
			set;
		}

        [IgnoreAutoMap]
        public virtual DateTime NoteDate {
            get => base.CreateDate; set => base.CreateDate = value;
        }

        public override void Calculate()
		{
			base.Calculate();
		}
	}
}
