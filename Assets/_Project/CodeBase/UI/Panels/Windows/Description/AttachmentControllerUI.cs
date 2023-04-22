using System.Collections.Generic;
using System.Linq;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.Utils.Extensions;
using UnityEngine;

namespace _Project.CodeBase.UI
{
    public class AttachmentControllerUI : Turnable
    {
        [SerializeField] private SlotSprite _slotSprite;
        [SerializeField] private List<AttachmentSlotUI> _slots;

        public void Setup(List<AttachmentType> types)
        {
            foreach (var (slot, i) in _slots.WithIndex())
            {
                var value = i < types.Count;
                slot.Turn(value);
                if (value)
                    slot.Setup(GetSprite(types[i]));
            }
        }

        private Sprite GetSprite(AttachmentType type) => 
            _slotSprite.First(x => x.Key == type).Value;
    }
}