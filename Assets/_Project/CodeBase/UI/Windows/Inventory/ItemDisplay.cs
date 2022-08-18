using System;
using _Project.CodeBase.UI.Elements.SpecificButtonLogic;
using _Project.CodeBase.Utils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows.Inventory
{
    public class ItemDisplay : MonoBehaviour
    {
        [SerializeField] private DraggableSlot _draggable;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _amount;

        public void Construct(Canvas canvas, InventorySlotUI slot, Action<InventorySlotUI> click)
        {
            _draggable.Construct(canvas);
            _draggable.Construct(slot, click);
        }

        public void UpdateSlotUI(Sprite icon, string text)
        {
            _icon.ChangeAlpha(icon is null ? 0f : 1f);
            _icon.sprite = icon;
            _amount.text = text;
        }
    }
}