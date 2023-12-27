using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class RevolverInfo : GunInfo
    {
        [SerializeField] private RevolverSpecs _revolverSpecs;
        
        public RevolverSpecs RevolverSpecs => _revolverSpecs;
    }
}