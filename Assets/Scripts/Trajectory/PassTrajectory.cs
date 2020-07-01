using Sirenix.OdinInspector;
using UnityEngine;

namespace Trajectory
{
    public sealed class PassTrajectory : MonoBehaviour
    {
        [SerializeField, Required] private Transform _pointPrefab = default;
        [SerializeField, Min(2)] private int _pointsCount = 100;
        [SerializeField] private int _frameModulo = 1;
    
        public Transform Source { get; set; }
        public Transform Destination { get; set; }
        public Vector3 IntermediateOffset { get; set; }

        [Button]
        public void Show(Transform source, Transform destination, Vector3 intermediateOffset)
        {
            Source = source;
            Destination = destination;
            IntermediateOffset = intermediateOffset;
            
            if (!enabled)
                enabled = true;
        }

        [Button]
        public void Hide()
        {
            Source = default;
            Destination = default;
            IntermediateOffset = Vector3.zero;
            enabled = false;
        }

        private void Update()
        {
            if (Time.frameCount % _frameModulo != 0) return;

            Refresh();
        }

        private void Refresh()
        {
            for (var index = 0; index < _pointsCount; index++)
            {
                var parameter = (float) index / (_pointsCount - 1);
                _points[index].position = BezierCurve.Quadratic(Source.position, Intermediate, Destination.position, parameter);
            }
        }

        private Vector3 Intermediate => Midpoint + IntermediateOffset;

        private Vector3 Midpoint => Vector3.Lerp(Source.position, Destination.position, 0.5f);

        private void OnEnable()
        {
            foreach (var point in _points)
            {
                point.gameObject.SetActive(true);
            }
            
            if (Source != null && Destination != null)
                Refresh();
        }

        private void OnDisable()
        {
            foreach (var point in _points)
            {
                point.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            Hide();
        }

        private void Awake()
        {
            _points = SpawnPoints();
        }

        private Transform[] SpawnPoints()
        {
            var points = new Transform[_pointsCount];

            for (var index = 0; index < points.Length; index++)
            {
                points[index] = Instantiate(_pointPrefab, transform);
            }

            return points;
        }

        private Transform[] _points;
    }
}