using UnityEngine;

namespace Controls
{
    public readonly struct ScreenPointArgs
    {
        public readonly Vector2 PointInViewport;

        public ScreenPointArgs(Vector2 pointInViewport)
        {
            PointInViewport = pointInViewport;
        }
    }
}