using System.Runtime.Serialization;

namespace easiplan.domain.Entities
{
	public class ClientChecklist : BaseEntity<int>
	{
		private bool _StatutoryNotice;

		private bool _RiskCompleted;

		private string _RiskStatus;

		private bool _FicaId;

		private bool _FicaUtility;

		private bool _FicaTaxNumber;

		private bool _FicaVAT;

		private bool _HasNoWill;

		private bool _HasWill;

		private bool _NewWill;

		private string _WillExecutor;

		private bool _NotesUT;

		private bool _NotesRA;

		private bool _NotesPF;

		private bool _NotesLA;

		private bool _NotesLife;

		private bool _NotesMed;

		private bool _InvUT;

		private bool _InvOffshore;

		private bool _InvLocal;

		private bool _InvGold;

		private bool _InvETF;

		private bool _FullAnalysis;

		[IgnoreDataMember]
		public virtual int ClientId
		{
			get;
			set;
		}

		public virtual bool StatutoryNotice
		{
			get
			{
				return _StatutoryNotice;
			}
			set
			{
				_StatutoryNotice = value;
				InvokePropertyChanged("StatutoryNotice");
			}
		}

		public virtual bool RiskCompleted
		{
			get
			{
				return _RiskCompleted;
			}
			set
			{
				_RiskCompleted = value;
				InvokePropertyChanged("RiskCompleted");
			}
		}

		public virtual string RiskStatus
		{
			get
			{
				return _RiskStatus;
			}
			set
			{
				_RiskStatus = value;
				InvokePropertyChanged("RiskStatus");
			}
		}

		public virtual bool FicaId
		{
			get
			{
				return _FicaId;
			}
			set
			{
				_FicaId = value;
				InvokePropertyChanged("FicaId");
			}
		}

		public virtual bool FicaUtility
		{
			get
			{
				return _FicaUtility;
			}
			set
			{
				_FicaUtility = value;
				InvokePropertyChanged("FicaUtility");
			}
		}

		public virtual bool FicaTaxNumber
		{
			get
			{
				return _FicaTaxNumber;
			}
			set
			{
				_FicaTaxNumber = value;
				InvokePropertyChanged("FicaTaxNumber");
			}
		}

		public virtual bool FicaVAT
		{
			get
			{
				return _FicaVAT;
			}
			set
			{
				_FicaVAT = value;
				InvokePropertyChanged("FicaVAT");
			}
		}

		public virtual bool HasNoWill
		{
			get
			{
				return _HasNoWill;
			}
			set
			{
				_HasNoWill = value;
				InvokePropertyChanged("HasNoWill");
			}
		}

		public virtual bool HasWill
		{
			get
			{
				return _HasWill;
			}
			set
			{
				_HasWill = value;
				InvokePropertyChanged("HasWill");
			}
		}

		public virtual bool NewWill
		{
			get
			{
				return _NewWill;
			}
			set
			{
				_NewWill = value;
				InvokePropertyChanged("NewWill");
			}
		}

		public virtual string WillExecutor
		{
			get
			{
				return _WillExecutor;
			}
			set
			{
				_WillExecutor = value;
				InvokePropertyChanged("WillExecutor");
			}
		}

		public virtual bool NotesUT
		{
			get
			{
				return _NotesUT;
			}
			set
			{
				_NotesUT = value;
				InvokePropertyChanged("NotesUT");
			}
		}

		public virtual bool NotesRA
		{
			get
			{
				return _NotesRA;
			}
			set
			{
				_NotesRA = value;
				InvokePropertyChanged("NotesRA");
			}
		}

		public virtual bool NotesPF
		{
			get
			{
				return _NotesPF;
			}
			set
			{
				_NotesPF = value;
				InvokePropertyChanged("NotesPF");
			}
		}

		public virtual bool NotesLA
		{
			get
			{
				return _NotesLA;
			}
			set
			{
				_NotesLA = value;
				InvokePropertyChanged("NotesLA");
			}
		}

		public virtual bool NotesLife
		{
			get
			{
				return _NotesLife;
			}
			set
			{
				_NotesLife = value;
				InvokePropertyChanged("NotesLife");
			}
		}

		public virtual bool NotesMed
		{
			get
			{
				return _NotesMed;
			}
			set
			{
				_NotesMed = value;
				InvokePropertyChanged("NotesMed");
			}
		}

		public virtual bool InvUT
		{
			get
			{
				return _InvUT;
			}
			set
			{
				_InvUT = value;
				InvokePropertyChanged("InvUT");
			}
		}

		public virtual bool InvOffshore
		{
			get
			{
				return _InvOffshore;
			}
			set
			{
				_InvOffshore = value;
				InvokePropertyChanged("InvOffshore");
			}
		}

		public virtual bool InvLocal
		{
			get
			{
				return _InvLocal;
			}
			set
			{
				_InvLocal = value;
				InvokePropertyChanged("InvLocal");
			}
		}

		public virtual bool InvGold
		{
			get
			{
				return _InvGold;
			}
			set
			{
				_InvGold = value;
				InvokePropertyChanged("InvGold");
			}
		}

		public virtual bool InvETF
		{
			get
			{
				return _InvETF;
			}
			set
			{
				_InvETF = value;
				InvokePropertyChanged("InvETF");
			}
		}

		public virtual bool FullAnalysis
		{
			get
			{
				return _FullAnalysis;
			}
			set
			{
				_FullAnalysis = value;
				InvokePropertyChanged("FullAnalysis");
			}
		}
	}
}
