using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    public class ScopeInfo : AttachmentInfo
    {
        [SerializeField] private ScopeSpecs _scopeSpecs;
        [SerializeField] private Color _dot;
        [SerializeField] private Sprite _crossHair;
        
        public ScopeSpecs ScopeSpecs => _scopeSpecs;
        public Color Dot => _dot;
        public Sprite CrossHair => _crossHair;
    }
}