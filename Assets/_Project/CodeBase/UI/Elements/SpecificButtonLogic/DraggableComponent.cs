using System;
using System.Collections.Generic;
using _Project.CodeBase.UI.Elements.Slot.Drop;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.CodeBase.UI.Elements.SpecificButtonLogic
{
    public class DraggableComponent : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        
        private Canvas _canvas;
        
        public event Action<PointerEventData> OnBeginDragHandler;
        public event Action<PointerEventData> OnDragHandler;
        public event Action<PointerEventData, bool> OnEndDragHandler;

        public bool FollowCursor { get; set; } = true;
        public bool CanDrag { get; set; } = true;
        public Vector3 StartPosition { get; set; }

        public void Construct(Canvas canvas) =>
            _canvas = canvas;
        
        public void OnInitializePotentialDrag(PointerEventData eventData) => 
            StartPosition = _rectTransform.anchoredPosition;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!CanDrag) return;
            OnBeginDragHandler?.Invoke(eventData);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (!CanDrag) return;
            
            OnDragHandler?.Invoke(eventData);

            if (FollowCursor) 
                _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!CanDrag) return;

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            var dropArea = FindDropArea(results);

            if (dropArea != null)
            {
                if (dropArea.Accepts(this))
                {
                    dropArea.Drop(this);
                    OnEndDragHandler?.Invoke(eventData, true);
                    return;
                }
            }

            _rectTransform.anchoredPosition = StartPosition;
            OnEndDragHandler?.Invoke(eventData, false);
        }

        private DropArea FindDropArea(List<RaycastResult> results)
        {
            DropArea dropArea = null;

            foreach (var result in results)
            {
                dropArea = result.gameObject.GetComponentInParent<DropArea>();

                if (dropArea != null)
                    break;
            }

            return dropArea;
        }
    }
}