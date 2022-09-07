// Decompiled with JetBrains decompiler
// Type: my.domain.lib.core.Domain.BaseValidator`2
// Assembly: my.domain.lib.core, Version=1.0.6659.30746, Culture=neutral, PublicKeyToken=null
// MVID: 4BBC88EF-DDEF-4B94-AC1F-3C173E27DA87
// Assembly location: C:\Users\Admins\Projects\FinX\BckUp\my.domain.lib.core.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my.domain.lib.core.Domain
{
    public abstract class BaseValidator<TClass, TKey> : IValidator<TClass>
    where TClass : class, IClass<TKey>
    where TKey : IEquatable<TKey>
    {
        public BaseValidator(BaseManager<TClass, TKey> manager)
        {
            if (manager == null)
                throw new ArgumentNullException(nameof(manager));
            this.Manager = manager;
        }

        private BaseManager<TClass, TKey> Manager { get; set; }

        public virtual async Task<DataResult> ValidateForInsertAsync(TClass item)
        {
            this.ValidateNotNull(item);
            DataResult dataResult = await this.ValidateExists(item, new List<DataError>(), false);
            return dataResult;
        }

        public virtual async Task<DataResult> ValidateForUpdateAsync(TClass item)
        {
            this.ValidateNotNull(item);
            DataResult dataResult = await this.ValidateExists(item, new List<DataError>(), true);
            return dataResult;
        }

        public virtual async Task<DataResult> ValidateForDeleteAsync(TClass item)
        {
            this.ValidateNotNull(item);
            DataResult dataResult = await this.ValidateExists(item, new List<DataError>(), true);
            return dataResult;
        }

        public async Task<DataResult> ValidateDuplicateName(
          TClass item,
          List<DataError> errors)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                errors.Add(new DataError(DataErrorDescEnum.CannotBeEmptyOrNull, "Name", new string[0]));
            }
            else
            {
                TClass @class = await this.Manager.FindByNameAsync(item.Name);
                TClass owner = @class;
                @class = default(TClass);
                if ((object)owner != null && !EqualityComparer<TKey>.Default.Equals(owner.Id, item.Id))
                    errors.Add(new DataError(DataErrorDescEnum.DuplicateFound, "Name", new string[0]));
                owner = default(TClass);
            }
            return errors.Count <= 0 ? DataResult.Success : DataResult.Failed(errors.ToArray());
        }

        public async Task<DataResult> ValidateExists(
          TClass item,
          List<DataError> errors,
          bool MustExist = false)
        {
            if (string.IsNullOrWhiteSpace(item.Id.ToString()))
            {
                errors.Add(new DataError(DataErrorDescEnum.CannotBeEmptyOrNull, "Id", new string[0]));
            }
            else
            {
                TClass @class = await this.Manager.FindByIdAsync(item.Id);
                TClass owner = @class;
                @class = default(TClass);
                if (MustExist && (object)owner == null)
                    errors.Add(new DataError(DataErrorDescEnum.NotFound, "Id", new string[0]));
                if (!MustExist && (object)owner != null)
                    errors.Add(new DataError(DataErrorDescEnum.AlreadyExist, "Id", new string[0]));
                owner = default(TClass);
            }
            return errors.Count <= 0 ? DataResult.Success : DataResult.Failed(errors.ToArray());
        }

        internal void ValidateNotNull(TClass item)
        {
            if ((object)item == null)
                throw new ArgumentNullException(nameof(item));
        }
    }
}
