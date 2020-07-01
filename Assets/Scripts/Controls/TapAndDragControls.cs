using System;
using Sirenix.OdinInspector;
using Trajectory;
using UnityEngine;

namespace Controls
{
    public sealed class TapAndDragControls : MonoBehaviour
    {
        [SerializeField, Required, HideInPrefabs] private TapAndDrag _tapAndDrag = default;
        [SerializeField, Required, HideInPrefabs] private PassToNextTarget _passToNextTarget = default;
        [SerializeField, Required, HideInPrefabs] private PassTrajectory _trajectory = default;
        [SerializeField] private Vector3 _maxIntermediateOffset = Vector3.right * 10f;
        [SerializeField] private Vector3 _extraOffset = Vector3.zero;
        [SerializeField, MinMaxSlider(-0.5f, 0.5f, ShowFields = true)] 
        private Vector2 _pointerOffsetLimits = new Vector2(-0.5f, 0.5f);

        private void OnEnable()
        {
            _tapAndDrag.Tap += _onTap;
            _tapAndDrag.Drag += _onDrag;
            _tapAndDrag.Release += _onRelease;
        }

        private void OnDisable()
        {
            _tapAndDrag.Tap -= _onTap;
            _tapAndDrag.Drag -= _onDrag;
            _tapAndDrag.Release -= _onRelease;
        }

        private void Awake()
        {
            _onTap = (sender, args) =>
            {
                _initialPoint = args.PointInViewport;
                ShowTrajectory(args.PointInViewport);
            };

            _onDrag = (sender, args) => ShowTrajectory(args.PointInViewport);

            _onRelease = (sender, args) =>
            {
                _trajectory.Hide();
                
                if (!_passToNextTarget.CanPass) return;

                var offset = ScreenPointToIntermediateOffset(args.PointInViewport);
                _passToNextTarget.Pass(offset + _extraOffset);
            };
        }

        private void ShowTrajectory(Vector2 point)
        {
            if (!_passToNextTarget.CanPass) return;
            
            var source = _passToNextTarget.Source;
            var destination = _passToNextTarget.Destination;
            var offset = ScreenPointToIntermediateOffset(point);
            _trajectory.Show(source, destination, offset);
        }

        private Vector3 ScreenPointToIntermediateOffset(Vector2 viewportPoint)
        {
            var offset = viewportPoint - _initialPoint;
            var xOffset = Mathf.Clamp(offset.x, _pointerOffsetLimits.x, _pointerOffsetLimits.y);
            var inverseLerp = Mathf.InverseLerp(_pointerOffsetLimits.x, _pointerOffsetLimits.y, xOffset);
            var intermediateFactor = inverseLerp * 2f - 1f;

            return intermediateFactor * _maxIntermediateOffset;
        }

        private Vector2 _initialPoint;
        private EventHandler<ScreenPointArgs> _onTap;
        private EventHandler<ScreenPointArgs> _onRelease;
        private EventHandler<ScreenPointArgs> _onDrag;
    }
}