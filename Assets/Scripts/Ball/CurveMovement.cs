using Sirenix.OdinInspector;
using UnityEngine;

namespace Ball
{
        public sealed class CurveMovement : MonoBehaviour
        {
                [SerializeField] private float _duration = 1f;
                
                public Transform Target;
        
                public Vector3 Source { get; private set; }
        
                public Vector3 Destination { get; private set; }
        
                public Vector3 Intermediate { get; private set; }
        
                public bool Moving { get; private set; }

                public Vector3 GetCurrentVelocity()
                {
                        if (!Moving) return Vector3.zero;

                        var from = PointForCurrentTime;
                        var to = Destination;
                        var timeLeft = Mathf.Clamp(_duration - _elapsedTime, 0f, _duration);
                        var velocity = (to - from) / timeLeft;

                        return velocity;
                }
        

                [Button, HideInEditorMode]
                public void StartMoving(Vector3 source, Vector3 intermediate, Vector3 destination)
                {
                        Source = source;
                        Intermediate = intermediate;
                        Destination = destination;
                
                        Moving = true;
                        _elapsedTime = 0f;
                }

                [Button, HideInEditorMode, ShowIf(nameof(Moving))]
                public void ForceStop() => Moving = false;

                private void Update()
                {
                        if (!Moving) return;

                        Target.position = PointForCurrentTime;
                        _elapsedTime += Time.deltaTime;

                        if (Mathf.Approximately((Target.position - Destination).sqrMagnitude, 0f))
                                Moving = false;
                }

                private Vector3 PointForCurrentTime => BezierCurve.Quadratic(Source, Intermediate, Destination, Parameter);

                private float Parameter => _elapsedTime / _duration;
                private float _elapsedTime;
        }
}

