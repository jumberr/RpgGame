using System;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class GunSettings
    {
        [SerializeField] private ScopeSpecs _scopeSpecs;
        [SerializeField] private AudioClip _shootSound;

        public ScopeSpecs ScopeSpecs => _scopeSpecs;
        public AudioClip ShootSound => _shootSound;
    }
}