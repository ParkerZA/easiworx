using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using System;
using System.Linq;

namespace easiplan.domain.Entities
{
	public class ClientMeetings : BaseEntity<int>
	{
		private DateTime _ScheduledDate;

		private DateTime _TimeFrom;

		private DateTime _TimeTo;

		private string _Notes = "";

		private double _DurationInt;

		private bool _Sync;

		private string _MeetingStatus;

		public virtual int ClientId
		{
			get;
			set;
		}

		public virtual int AgentId
		{
			get;
			set;
		}

		public virtual DateTime ScheduledDate
		{
			get
			{
				return _ScheduledDate;
			}
			set
			{
				if (_ScheduledDate == value) return;
				_ScheduledDate = value;

				Calculate();
				InvokePropertyChanged("ScheduledDate");
			}
		}

		public virtual DateTime TimeFrom
		{
			get
			{
				return _TimeFrom;
			}
			set
			{
				if (_TimeFrom == value) return;
				_TimeFrom = value;

				Calculate();
				InvokePropertyChanged("TimeFrom");
			}
		}

		public virtual DateTime TimeTo
		{
			get
			{
				return _TimeTo;
			}
			set
			{
				if (_TimeTo == value) return;
				_TimeTo = value;

				Calculate();
				InvokePropertyChanged("TimeTo");
			}
		}

		[NHMaxLengthAttr(4000)]
		public virtual string Notes
		{
			get
			{
				return _Notes;
			}
			set
			{
				_Notes = value;
				InvokePropertyChanged("Text");
			}
		}

		public virtual double _Duration
		{
			get
			{
				return _DurationInt;
			}
			set
			{
				if (_DurationInt == value) return;
				_DurationInt = value;

				Calculate();
				InvokePropertyChanged("_Duration");
			}
		}

		public virtual bool SetReminder
		{
			get;
			set;
		}

		public virtual double Reminder
		{
			get;
			set;
		}

		public virtual string Venue
		{
			get;
			set;
		}

		public virtual bool Sync
		{
			get
			{
				return _Sync;
			}
			set
			{
				_Sync = value;
				InvokePropertyChanged("Sync");
			}
		}

        public virtual string MeetingType
        {
            get;
            set;
        }
        public virtual string MeetingStatus
		{
			get
			{
				return _MeetingStatus;
			}
			set
			{
				_MeetingStatus = value;
				InvokePropertyChanged("MeetingStatus");
			}
		}

		public ClientMeetings()
		{
			ScheduledDate = DateTime.Now;
			SetReminder = true;
			Reminder = 15.0;
			MeetingStatus = MeetingStatusEnum.Scheduled.ToString();
			TimeFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
			TimeTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 30, 0);
		}

		public override void Calculate()
		{
			try
			{
				if (!InvokeModelCalculating(new EntityEventArgs()))
					return;


				DateTime dateTime = TimeTo;
				_Duration = dateTime.Subtract(TimeFrom).TotalHours;


				dateTime = ScheduledDate;
				ScheduledDate = DateTime.Parse(dateTime.ToShortDateString());

				dateTime = ScheduledDate;
				string arg = dateTime.ToShortDateString();

				dateTime = TimeFrom;
				TimeFrom = DateTime.Parse($"{arg} {dateTime.ToShortTimeString()}");

				dateTime = ScheduledDate;
				string arg2 = dateTime.ToShortDateString();
				dateTime = TimeTo;
				TimeTo = DateTime.Parse($"{arg2} {dateTime.ToShortTimeString()}");

				if (ScheduledDate < DateTime.Now)
					MeetingStatus = MeetingStatusEnum.Expired.ToString();

				InvokeModelCalculated();
			}
			catch (Exception)
			{
			}
		}
	}
}
