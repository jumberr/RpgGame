using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.Windows.Inventory
{
    public class InventoryActionUI : MonoBehaviour
    {
        public TMP_Text Text;
        public Button Button;
        
        public void Construct(Action click, string text)
        {
            Text.text = text;
            Button.onClick.AddListener(() => click?.Invoke());
        }

        private void OnDisable() => 
            Button.onClick.RemoveAllListeners();
    }
}