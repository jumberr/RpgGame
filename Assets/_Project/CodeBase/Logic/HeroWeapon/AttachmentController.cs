using System.Collections.Generic;
using _Project.CodeBase.Logic.HeroWeapon.Data;
using _Project.CodeBase.StaticData;
using _Project.CodeBase.Utils;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class AttachmentController
    {
        private readonly Dictionary<AttachmentType, GameObject> _attachments = new Dictionary<AttachmentType, GameObject>();
        private CurrentItem _currentItem;
        private WeaponData _weaponData;
        private GunSpecs _gunSpecs;


        public void Setup(CurrentItem currentItem, WeaponData weaponData)
        {
            _currentItem = currentItem;
            _weaponData = weaponData;
        }

        public void CreateAttachment(AttachmentInfo attachmentInfo)
        {
            if (!ItemInfoUtils.IsGun(_currentItem.Info, out var gunInfo)) return;

            _gunSpecs = gunInfo.GetSpecs();
            var attachmentPossibility = _gunSpecs.GetPossibility(attachmentInfo.Type);
            if (!attachmentPossibility.Possibility) return;
            if (_attachments.ContainsKey(attachmentInfo.Type))
            {
                Debug.Log($"<color=red> You are trying to add attachment (type: {attachmentInfo.Type}), but another one is still attached.</color>");
                return;
            }

            var attachmentObj = Object.Instantiate(attachmentInfo.Prefab,
                attachmentPossibility.Position + attachmentInfo.OffsetPosition,
                Quaternion.identity, _currentItem.WeaponConfiguration.AttachmentsParent);
            AddAttachment(attachmentInfo, attachmentObj);
        }

        public void DestroyAllAttachments()
        {
            foreach (var attachment in _attachments) 
                DestroyAttachment(attachment.Key);
        }
        
        public void DestroyAttachment(AttachmentType type)
        {
            if (!_attachments.ContainsKey(type)) return;
            Object.Destroy(_attachments[type]);
            RemoveAttachment(type);
        }

        private void AddAttachment(AttachmentInfo info, GameObject attachmentObj)
        {
            _weaponData.AddAttachment(info.Type, info.UIInfo.Name);
            _attachments.Add(info.Type, attachmentObj);
        }

        private void RemoveAttachment(AttachmentType type)
        {
            _weaponData.RemoveAttachment(type);
            _attachments.Remove(type);
        }
    }
}