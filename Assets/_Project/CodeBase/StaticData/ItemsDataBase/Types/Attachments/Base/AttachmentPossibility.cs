using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class AttachmentPossibility
    {
        [SerializeField] private AttachmentType _type;
        [SerializeField] private bool _possibility;
        [ShowIf("_possibility"), SerializeField] private Vector3 _position; // relative to "Attachments" transform in gun

        public AttachmentType Type => _type;
        public bool Possibility => _possibility;
        public Vector3 Position => _position; 
    }
}