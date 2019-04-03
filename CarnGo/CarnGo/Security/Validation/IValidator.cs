using System.Collections.Generic;

namespace CarnGo.Security
{
    public interface IValidator
    {
        List<string> ValidationErrorMessages { get; }
        bool Validate();
    }
}