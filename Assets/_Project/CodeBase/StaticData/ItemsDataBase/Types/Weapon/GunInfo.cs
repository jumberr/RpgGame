using System;
using System.Collections.Generic;
using _Project.CodeBase.Logic.Inventory;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class GunInfo : WeaponInfo
    {
        [SerializeField] private GunSpecs _gunSpecs;
        [SerializeField] private List<ItemName> _possibleAttachments;
        [SerializeField] private List<ItemName> _defaultAttachments;
        [SerializeField] private GunSettings _defaultSettings;
        [SerializeField] private AttachmentPossibilityContainer _attachmentPossibility;

        public GunSpecs GunSpecs => _gunSpecs;
        public List<ItemName> PossibleAttachments => _possibleAttachments;
        public List<ItemName> DefaultAttachments => _defaultAttachments;
        public GunSettings DefaultSettings => _defaultSettings;
        public AttachmentPossibilityContainer AttachmentPossibility => _attachmentPossibility;

        public AttachmentPossibility GetPossibility(AttachmentType type) => 
            _attachmentPossibility.GetItemBy(type);
    }
}