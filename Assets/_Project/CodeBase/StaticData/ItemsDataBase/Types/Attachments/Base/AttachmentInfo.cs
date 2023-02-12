using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    public class AttachmentInfo : ItemInfo
    {
        [SerializeField] private AttachmentType _type;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Vector3 _offsetPosition;
        
        public AttachmentType Type => _type;
        public GameObject Prefab => _prefab;
        public Vector3 OffsetPosition => _offsetPosition;
    }
}