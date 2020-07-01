using Ball;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Assertions;
using Validation;

[RequireComponent(typeof(PassSequence))]
public sealed class PassToNextTarget : MonoBehaviour
{
    [SerializeField, Required] private BallMovement _ballMovement = default;

    public bool CanPass => !CurveMovement.Moving &&
                           !_passSequence.Finished &&
                           _ballMovement.Ball.CanBePassed;

    public Transform Source => _ballMovement.Ball.transform;
    public Transform Destination => _passSequence.TryGetNext(out var nextTarget) ? nextTarget.transform : default;

    [Button, HideInEditorMode, ShowIf(nameof(CanPass))]
    public void Pass(Vector3 intermediateOffset)
    {
        Assert.IsTrue(CanPass);
        if (!_passSequence.TryGetNext(out var nextTarget)) return;

        var source = Source.position;
        var destination = Destination.position;
        var intermediate = Vector3.Lerp(source, destination, 0.5f) + intermediateOffset;
        CurveMovement.StartMoving(source, intermediate, destination);
        
        _passSequence.GoToNext();
    }

    private CurveMovement CurveMovement => _ballMovement.Curve;
    
    private void Awake()
    {
        gameObject.Require(out _passSequence);
    }

    private PassSequence _passSequence;
}