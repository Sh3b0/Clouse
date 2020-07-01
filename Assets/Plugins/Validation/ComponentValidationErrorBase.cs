using System;
using Object = UnityEngine.Object;

namespace Validation
{
    public abstract class ComponentValidationErrorBase : Exception
    {
        protected ComponentValidationErrorBase(string message) : base(message)
        {
            
        }

        protected static string FormatMessage(Object context, Type componentType)
        {
            return $"Component of type {componentType.Name} was not found in {context.name}";
        }
    }
}