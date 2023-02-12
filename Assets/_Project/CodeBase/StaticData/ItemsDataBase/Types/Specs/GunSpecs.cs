using System;
using _Project.CodeBase.Logic.HeroWeapon;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.StaticData
{
    [Serializable]
    public class GunSpecs
    {
        [SerializeField] private Vector3 _recoil;
        [SerializeField] private Vector3 _aimRecoil;
        
        [ProgressBar(0, 20, r: 0, g: 1f, b: 0.2f)]
        [SerializeField] private float _snappiness;
        
        [ProgressBar(0, 20, r: 0, g: 1f, b: 0.2f)]
        [SerializeField] private float _returnSpeed;
        
        [SerializeField] private bool _isAutomatic;

        [ProgressBar(0, 15, r: 0, g: 0.2f, b: 0.75f)]
        [SerializeField] private float _reloadTime;
        
        [ProgressBar(0, 15, r: 0, g: 0.2f, b: 0.75f)]
        [SerializeField] private float _fullReloadTime;

        [ProgressBar(0, 1, r: 0.4f, g: 0.2f, b: 0.75f)]
        [SerializeField] private float _accuracy;
        
        [ProgressBar(0, 1, r: 0.4f, g: 0.2f, b: 0.75f)]
        [SerializeField] private float _aimAccuracy;
        
        [ProgressBar(0, 100, r: 0.4f, g: 0.2f, b: 0.75f)]
        [SerializeField] private float _accuracyDistance;

        [SerializeField] private AmmoType _ammoType;
        [SerializeField] private bool _scoping;
        [SerializeField] private MagazineInfo _magazineInfo;
        
        public Vector3 Recoil => _recoil;
        public Vector3 AimRecoil => _aimRecoil;
        public float Snappiness => _snappiness;
        public float ReturnSpeed => _returnSpeed;
        public bool IsAutomatic => _isAutomatic;
        public float ReloadTime => _reloadTime;
        public float FullReloadTime => _fullReloadTime;
        public float Accuracy => _accuracy;
        public float AimAccuracy => _aimAccuracy;
        public float AccuracyDistance => _accuracyDistance;
        public AmmoType AmmoType => _ammoType;
        public bool Scoping => _scoping;
        public MagazineInfo MagazineInfo => _magazineInfo;
    }
}