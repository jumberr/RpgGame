using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class GunSettings
    {
        [SerializeField] private ScopeSpecs _scopeSpecs;

        public ScopeSpecs ScopeSpecs => _scopeSpecs;
    }
}