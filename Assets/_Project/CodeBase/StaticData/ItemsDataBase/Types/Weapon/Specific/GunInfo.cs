using System;
using System.Collections.Generic;
using _Project.CodeBase.Logic.Inventory;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class GunInfo : WeaponInfo<GunSpecs>
    {
        [SerializeField] private GunSpecs _gunSpecs;
        [SerializeField] private List<ItemName> _possibleAttachments; // todo attachments move in another cfg
        [SerializeField] private List<ItemName> _defaultAttachments;
        [SerializeField] private GunSettings _defaultSettings;
        [SerializeField] private AttachmentPossibilityContainer _attachmentPossibility;
        
        public List<ItemName> PossibleAttachments => _possibleAttachments;
        public List<ItemName> DefaultAttachments => _defaultAttachments;
        public GunSettings DefaultSettings => _defaultSettings;
        public AttachmentPossibilityContainer AttachmentPossibility => _attachmentPossibility;

        public AttachmentPossibility GetPossibility(AttachmentType type) => 
            _attachmentPossibility.GetItemBy(type);

        public override GunSpecs GetSpecs() => 
            _gunSpecs;
    }
}