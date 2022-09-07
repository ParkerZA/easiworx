using my.domain.lib.core.Attributes;
using System.ComponentModel.DataAnnotations;

namespace easiplan.domain.Entities
{
	public class EmailTemplate : BaseEntity<int>
	{
		private string _TemplateName;

		private string _TemplateSubject;

		private string _TemplateBody;

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

		[Required(ErrorMessage = "Email Subject is Required")]
		public virtual string TemplateSubject
		{
			get
			{
				return _TemplateSubject;
			}
			set
			{
				_TemplateSubject = value;
				InvokePropertyChanged("TemplateSubject");
			}
		}

		[NHMaxLengthAttr(4000)]
		public virtual string TemplateBody
		{
			get
			{
				return _TemplateBody;
			}
			set
			{
				_TemplateBody = value;
				InvokePropertyChanged("TemplateBody");
			}
		}

		public EmailTemplate()
		{
			Status = "1";
		}

		public override void Calculate()
		{
			base.Calculate();
		}

		public override void Validate(string FieldName = null)
		{
			base.Validate(FieldName);
		}
	}
}
