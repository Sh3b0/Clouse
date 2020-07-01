using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public static class ComponentExtensions
{
        public static T[] SortedByHierarchy<T>([NotNull] this IEnumerable<T> components) where T : MonoBehaviour
        {
                if (components == null) throw new ArgumentNullException(nameof(components));

                return components
                        .OrderBy(c => c.transform.GetSiblingIndex())
                        .ToArray();
        }
}