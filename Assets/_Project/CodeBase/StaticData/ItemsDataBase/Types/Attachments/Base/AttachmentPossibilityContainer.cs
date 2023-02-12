using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class AttachmentPossibilityContainer
    {
        [SerializeField] private List<AttachmentPossibility> _list = new List<AttachmentPossibility>();

        public AttachmentPossibility GetItemBy(AttachmentType type) => 
            _list.Find(x => x.Type == type);

        public List<AttachmentType> GetPossibleTypes() => 
            (from attachmentPossibility in _list where attachmentPossibility.Possibility select attachmentPossibility.Type).ToList();
    }
}