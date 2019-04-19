using System.Collections.Generic;

namespace CarnGo.Security
{
    public interface IValidator<T>
    {
        List<string> ValidationErrorMessages { get; }
        bool Validate(T toValidate);
    }
}