using System;
using _Project.CodeBase.UI.Elements.SpecificButtonLogic;
using _Project.CodeBase.Utils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows.Inventory
{
    public class InventorySlotUI : MonoBehaviour
    {
        public Image Icon;
        public TMP_Text Amount;
        public Button Button;
        public HoldButtonUI HoldButtonUI;
        [HideInInspector] public int SlotID;
        
        public event Action<InventorySlotUI> OnClick;

        public void Construct(int id, Action<InventorySlotUI> handleClick)
        {
            SlotID = id;
            OnClick += handleClick;
        }

        public void OnEnable() => 
            Button.onClick.AddListener(ClickInvoke);

        public void OnDisable() => 
            Button.onClick.RemoveListener(ClickInvoke);

        public void UpdateSlotUI(Sprite icon, string text)
        {
            Icon.ChangeAlpha(icon is null ? 0f : 1f);
            Icon.sprite = icon;
            Amount.text = text;
        }
        
        private void ClickInvoke() => 
            OnClick?.Invoke(this);
    }
}