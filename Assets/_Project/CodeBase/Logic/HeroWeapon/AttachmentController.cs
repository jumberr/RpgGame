using System.Collections.Generic;
using _Project.CodeBase.Logic.HeroWeapon.Data;
using _Project.CodeBase.StaticData;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    public class AttachmentController
    {
        private readonly Dictionary<AttachmentType, GameObject> _attachments = new Dictionary<AttachmentType, GameObject>();
        private CurrentWeapon _currentWeapon;
        private WeaponData _weaponData;

        public void Construct(CurrentWeapon currentWeapon, WeaponData weaponData)
        {
            _currentWeapon = currentWeapon;
            _weaponData = weaponData;
        }

        public void CreateAttachment(AttachmentInfo attachmentInfo)
        {
            var attachmentPossibility = _currentWeapon.GunInfo.GetPossibility(attachmentInfo.Type);
            if (!attachmentPossibility.Possibility) return;
            if (_attachments.ContainsKey(attachmentInfo.Type))
            {
                Debug.Log($"<color=red> You are trying to add attachment (type: {attachmentInfo.Type}), but another one is still attached.</color>");
                return;
            }

            var attachmentGO = Object.Instantiate(attachmentInfo.Prefab, attachmentPossibility.Position + attachmentInfo.OffsetPosition,
                Quaternion.identity, _currentWeapon.WeaponConfiguration.AttachmentsParent);
            AddAttachment(attachmentInfo, attachmentGO);
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

        private void AddAttachment(AttachmentInfo info, GameObject attachmentGO)
        {
            _weaponData.AddAttachment(info.Type, info.UIInfo.Name);
            _attachments.Add(info.Type, attachmentGO);
        }

        private void RemoveAttachment(AttachmentType type)
        {
            _weaponData.RemoveAttachment(type);
            _attachments.Remove(type);
        }
    }
}