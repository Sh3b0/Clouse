using System;
using UnityEngine;

public sealed class PassSequence : MonoBehaviour
{
    public int NextIndex { get; private set; }
    public bool Finished => NextIndex >= _targets.Length;

    public bool TryGetNext(out PassTarget nextTarget)
    {
        if (Finished)
        {
            nextTarget = default;
            return false;
        }

        nextTarget = _targets[NextIndex];
        return true;
    }

    public void GoToNext() => NextIndex++;
    
    private void Awake()
    {
        _targets = GetComponentsInChildren<PassTarget>()
            .SortedByHierarchy();
    }

    private void OnDrawGizmos()
    {
        if (_targets == null) return;
        if (!TryGetNext(out var nextTarget)) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(nextTarget.Position, Vector3.up);
    }

    private PassTarget[] _targets;
}