using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI
{
    public class AttachmentSlotUI : Turnable
    {
        [SerializeField] private Image _icon;

        public void Setup(Sprite sprite) => 
            _icon.sprite = sprite;
    }
}