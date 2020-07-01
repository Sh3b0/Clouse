using System;
using Object = UnityEngine.Object;

namespace Validation
{
    public sealed class ParentComponentValidationError : ComponentValidationErrorBase
    {
        public ParentComponentValidationError(Object context, Type componentType) : 
            base(FormatMessage(context, componentType) + " nor in its parent.")
        {
        }
    }
}