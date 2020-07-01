using System;
using UnityEngine;

public class BezierCurveComponent : MonoBehaviour
{
    [SerializeField] private Vector3[] _points = new Vector3[2];

    public Vector3[] Points
    {
        get => _points;
        set
        { 
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.Length < 2) throw new ArgumentException(nameof(value));

            _points = value;
        }
    }

    public Vector3 EvaluateAt(float t) => BezierCurve.General(_points, t);
}