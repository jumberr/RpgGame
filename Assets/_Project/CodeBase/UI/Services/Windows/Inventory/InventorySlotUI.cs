using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Services.Windows.Inventory
{
    public class InventorySlotUI : MonoBehaviour
    {
        public Image Icon;
        public TMP_Text Amount;
        public Button Button;
        public int SlotID;
        
        public event Action<InventorySlotUI> OnClick;

        public void Construct(int id, Action<InventorySlotUI> handleClick)
        {
            SlotID = id;
            OnClick += handleClick;
        }

        public void OnEnable() => 
            Button.onClick.AddListener(() => OnClick?.Invoke(this));

        public void OnDisable() => 
            Button.onClick.RemoveAllListeners();
    }
}