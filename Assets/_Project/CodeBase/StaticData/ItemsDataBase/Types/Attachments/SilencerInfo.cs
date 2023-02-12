using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    public class SilencerInfo : AttachmentInfo
    {
        [SerializeField] private AudioClip _silencedClip;
        
        public AudioClip SilencedClip => _silencedClip;
    }
}