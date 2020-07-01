using System;
using Object = UnityEngine.Object;

namespace Validation
{
    public sealed class ComponentValidationError : ComponentValidationErrorBase
    {
        public ComponentValidationError(Object context, Type componentType) : 
            base(FormatMessage(context, componentType) + ".")
        {
            
        }
    }
}