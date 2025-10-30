using UniRx;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PacmanSailor.Scripts.UI.Components
{
    public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _knob;

        public readonly Subject<Vector2> OnSelectDirection = new();

        private RectTransform _rectTransform;
        private float _handleRange;
        private bool _isDragging;

        private void Awake()
        {
            _rectTransform = (RectTransform)transform;
            _handleRange = _rectTransform.rect.width / 2 - _knob.rect.width / 2;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDragging) return;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out var localPosition
            );

            if (localPosition.magnitude > _handleRange)
            {
                localPosition = localPosition.normalized * _handleRange;
            }

            _knob.anchoredPosition = localPosition;

            SelectDirection();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
            _knob.anchoredPosition = Vector2.zero;
        }

        private void SelectDirection()
        {
            if (math.abs(_knob.anchoredPosition.x) > math.abs(_knob.anchoredPosition.y))
            {
                switch (_knob.anchoredPosition.x)
                {
                    case > 0:
                        OnSelectDirection.OnNext(Vector2.right);
                        break;
                    case < 0:
                        OnSelectDirection.OnNext(Vector2.left);
                        break;
                }
            }
            else if (math.abs(_knob.anchoredPosition.y) > math.abs(_knob.anchoredPosition.x))
            {
                switch (_knob.anchoredPosition.y)
                {
                    case > 0:
                        OnSelectDirection.OnNext(Vector2.up);
                        break;
                    case < 0:
                        OnSelectDirection.OnNext(Vector2.down);
                        break;
                }
            }
        }
    }
}