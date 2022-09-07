using my.domain.lib.core.Attributes;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace easiplan.domain.Entities
{
	public class ManualsTemplate : BaseEntity<int>
	{
		private string _TemplateName;

		private string _TemplateDescription;

		private string _SourceFilename;

		private byte[] _TemplateBytes;

		[Required(ErrorMessage = "Template name is Required")]
		public virtual string TemplateName
		{
			get
			{
				return _TemplateName;
			}
			set
			{
				_TemplateName = value;
				InvokePropertyChanged("TemplateName");
			}
		}

		public virtual string TemplateDescription
		{
			get
			{
				return _TemplateDescription;
			}
			set
			{
				_TemplateDescription = value;
				InvokePropertyChanged("TemplateDescription");
			}
		}

		[Required(ErrorMessage = "Source file is Required")]
		public virtual string SourceFilename
		{
			get
			{
				return _SourceFilename;
			}
			set
			{
				_SourceFilename = value;
				InvokePropertyChanged("SourceFilename");
			}
		}

		public virtual byte[] TemplateBytes
		{
			get
			{
				return _TemplateBytes;
			}
			set
			{
				_TemplateBytes = value;
				InvokePropertyChanged("TemplateBytes");
			}
		}

		[IgnoreAutoMap]
		public virtual string Filename
		{
			get;
			set;
		}

		public ManualsTemplate()
		{
			Status = "True";
		}

		public override void Calculate()
		{
			if (!string.IsNullOrEmpty(SourceFilename))
			{
				Filename = new FileInfo(SourceFilename).Name;
			}
			if (Status == "1")
			{
				Status = "True";
			}
			if (string.IsNullOrEmpty(Status) || Status == "0")
			{
				Status = "False";
			}
			base.Calculate();
		}
	}
}
