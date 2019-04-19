using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using CarnGo.Security;

namespace CarnGo
{
    public class BaseViewModelWithValidation : BaseViewModel, INotifyDataErrorInfo
    {
        protected readonly Dictionary<string, List<string>> ErrorsDictionary = new Dictionary<string, List<string>>();
        public void Validate<T>(string propertyName, T propertyValue, IValidator<T> validator)
        {
            var propertyErrors = GetErrors(propertyName) as List<string>;
            if (propertyErrors == null)
            {
                propertyErrors = new List<string>();
            }
            else
            {
                propertyErrors.Clear();
            }

            if (validator.Validate(propertyValue) == false)
            {
                propertyErrors.AddRange(validator.ValidationErrorMessages);
            }

            ErrorsDictionary[propertyName] = propertyErrors;

            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        public IEnumerable GetErrors(string propertyName)
        {
            return ErrorsDictionary.TryGetValue(propertyName, out var errorsForProperty) ? errorsForProperty : new List<string>();
        }

        public bool HasErrors => ErrorsDictionary.Values.FirstOrDefault(err => err.Count > 0) != null;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        

    }
}