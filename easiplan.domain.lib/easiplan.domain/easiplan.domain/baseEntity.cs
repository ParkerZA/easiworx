using my.domain.lib.core.Attributes;
using my.domain.lib.core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace easiplan.domain
{
	/// <summary>
	/// Abstract Entity serves as base class ...all entities must derive from this class
	/// </summary>
	/// <typeparam name="TId"></typeparam>
	public abstract class BaseEntity<TId> : EntityTypedId<TId>
	{
		public delegate void RefreshEventHandler(object sender, EventArgs args);

		private bool _IsLoading = false;
		private bool _IsCalulating = false;

		[IgnoreDataMember]
		[IgnoreAutoMap]
		public virtual DateTime MinDateTime
		{
			get
			{
				return DateTime.Parse("1/1/1753");
			}
		}

        [IgnoreDataMember]
        [IgnoreAutoMap]
        public virtual bool IsCalculating
        {
            get
            {
                return _IsCalulating;
            }
            set
            {
                _IsCalulating = value;
            }
        }

         [IgnoreDataMember]
		[IgnoreAutoMap]
		public virtual bool IsLoading
		{
			get
			{
				return _IsLoading;
			}
			set
			{
				_IsLoading = value;
			}
		}

        public virtual event EventHandler<EntityEventArgs> ModelCalculating;

        public virtual event EventHandler<EntityEventArgs> ModelCalculated;

        public virtual event EventHandler<EntityEventArgs> ModelMapped;

        public virtual event RefreshEventHandler RefreshEvent;

		public BaseEntity()
		{
			CreateDate = DateTime.Now;
		}

        public virtual void Calculating()
        {

            if (IsLoading)
                return;

            InvokeModelCalculating(new EntityEventArgs());
        }

        public virtual void Calculate()
        {
        }

        public virtual void Validate(string FieldName = null)
		{
			ValidationContext context = new ValidationContext(this, null, null);
			List<ValidationResult> results = new List<ValidationResult>();
			bool isValid = true;
			if (!string.IsNullOrEmpty(FieldName))
			{
				Type entityType = GetType();
				PropertyInfo property = entityType.GetProperty(FieldName);
				if (property != (PropertyInfo)null)
				{
					object value = property.GetValue(this, null);
					context.MemberName = FieldName;
					isValid = Validator.TryValidateProperty(value, context, results);
				}
				InvokeEntityValidated(new EntityEventArgs
				{
					PropertyName = FieldName
				});
			}
			else
			{
				isValid = Validator.TryValidateObject(this, context, results);
				InvokeEntityValidated(new EntityEventArgs());
			}
			if (!isValid)
			{
				throw new MyValidationException(string.Format(results[0].ErrorMessage));
			}
		}

		public virtual void Initialise(bool isLoading=false)
		{
			IsLoading = isLoading;
		}

        public virtual bool InvokeModelCalculating(EntityEventArgs e = null)
        {
            if (IsLoading || IsCalculating)
                return false;

            IsCalculating = true;
            IsLoading = true;

            this.ModelCalculating?.Invoke(this, e);

            return true;
        }

        public virtual bool InvokeModelCalculated(EntityEventArgs e = null)
        {
            IsCalculating = false;
            IsLoading = false;

            this.ModelCalculated?.Invoke(this, e);

            return true;
        }

        public virtual void InvokeModelMapped(EntityEventArgs e = null)
		{
			this.ModelMapped?.Invoke(this, e);
		}

		public override void InvokePropertyChanged(string propertyName)
		{
			if (!IsLoading && !string.IsNullOrEmpty(propertyName))
			{
                try
                {

                    IsLoading = true;

                    base.InvokePropertyChanged(propertyName);

                }
                catch (MyValidationException vx)
                {
                    //TO DO : Handle validation exception
                }
                catch (Exception x)
                {
                    //TO DO : Handle unknown exception
                }
                finally
                {
                    IsLoading = false;
                   
                }
			}
		}

		public virtual void Refresh()
		{
			this.RefreshEvent?.Invoke(this, new EventArgs());
		}
	}
}
