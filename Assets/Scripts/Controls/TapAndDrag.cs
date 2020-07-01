using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controls
{
    public sealed class TapAndDrag : MonoBehaviour, 
        IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IDragHandler
    {
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (_pointerId != null) return;

            _pointerId = eventData.pointerId;
            TriggerEvent(Tap, eventData);
        }

        private void TriggerEvent(EventHandler<ScreenPointArgs> @event, PointerEventData eventData)
        {
            var viewPortPosition = ToViewPort(eventData.position);
            @event.Invoke(this, new ScreenPointArgs(viewPortPosition));
        }

        private Vector2 ToViewPort(Vector2 screenPosition) => 
            _camera.ScreenToViewportPoint(screenPosition);

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData) => OnRelease(eventData);

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData) => OnRelease(eventData);

        private void OnRelease(PointerEventData eventData)
        {
            if (_pointerId != eventData.pointerId) return;

            _pointerId = null;
            TriggerEvent(Release, eventData);;
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (_pointerId == eventData.pointerId) 
                TriggerEvent(Drag, eventData);
        }

        public event EventHandler<ScreenPointArgs> Tap;
        public event EventHandler<ScreenPointArgs> Drag; 
        public event EventHandler<ScreenPointArgs> Release; 

        private void Awake()
        {
            _camera = Camera.main;
        }

        private int? _pointerId;
        private Camera _camera;
    }
}