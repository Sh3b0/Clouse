using System;
using Object = UnityEngine.Object;

namespace Validation
{
    sealed class ChildrenComponentValidationError : ComponentValidationErrorBase
    {
        public ChildrenComponentValidationError(Object context, Type componentType) : 
            base(FormatMessage(context, componentType) + " nor in its children.")
        {
        }
    }
}