using System;
using System.Collections.Generic;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData;

namespace _Project.CodeBase.Logic
{
    [Serializable]
    public class WeaponData
    {
        public readonly Dictionary<AttachmentType, ItemName> Attachments = new Dictionary<AttachmentType, ItemName>();
        public bool Modified;

        public void SetModified(bool modified) => 
            Modified = modified;

        public void AddAttachment(AttachmentType type, ItemName name)
        {
            RemoveAttachment(type);
            Attachments.Add(type, name);
        }
        
        public void RemoveAttachment(AttachmentType type)
        {
            if (Attachments.ContainsKey(type))
                Attachments.Remove(type);
        }
    }
}