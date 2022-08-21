using System.Collections.Generic;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.UI.Elements.Slot.Drop;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Elements.SpecificButtonLogic
{
    public class DraggableComponent : MonoBehaviour, ICanBeDragged
    {
        [SerializeField] private Image _icon;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private bool _followCursor;

        private Canvas _canvas;
        private InventorySlot _dataBuffer;
        private Transform _uiRoot;

        public IDropArea DropArea { get; private set; }
        public bool CanDrag { get; set; } = true;

        public void Construct(Canvas canvas, Transform uiRoot, IDropArea dropArea)
        {
            _uiRoot = uiRoot;
            _canvas = canvas;
            DropArea = dropArea;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!CanDrag) return;

            _dataBuffer = DropArea.SlotData;
            DropArea.Release();
            transform.SetParent(_uiRoot);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (!CanDrag) return;
            
            if (_followCursor)
                _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!CanDrag) return;

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            var dropArea = FindDropArea(results) ?? DropArea;
            if (dropArea.Accepts(this))
            {
                dropArea.Drop(_dataBuffer);
                _dataBuffer = null;
                Destroy(gameObject);
            }
        }

        private IDropArea FindDropArea(List<RaycastResult> results)
        {
            IDropArea dropArea = null;

            foreach (var result in results)
            {
                dropArea = result.gameObject.GetComponentInParent<IDropArea>();

                if (dropArea != null)
                    break;
            }

            return dropArea;
        }

        public void ChangeIcon(Sprite icon) =>
            _icon.sprite = icon;
    }
}