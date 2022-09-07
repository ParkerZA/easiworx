using my.domain.lib.core.Attributes;
using System.ComponentModel.DataAnnotations;

namespace easiplan.domain.Entities
{
	public class SmsTemplate : BaseEntity<int>
	{
		private string _TemplateName;

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

		public SmsTemplate()
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
