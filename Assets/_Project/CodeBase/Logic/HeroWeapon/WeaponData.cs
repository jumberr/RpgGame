using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.CodeBase.Logic.HeroWeapon
{
    [Serializable]
    public class WeaponData
    {
        // Recoil Settings
        public Vector3 Recoil;
        public Vector3 AimRecoil;
        
        [ProgressBar(0, 20, r: 0, g: 1f, b: 0.2f)]
        public float Snappiness;
        
        [ProgressBar(0, 20, r: 0, g: 1f, b: 0.2f)]
        public float ReturnSpeed;
        
        public bool IsAutomatic;
        
        [ProgressBar(0, 100, r: 1, g: 0f, b: 0f)]
        public float Damage;
        
        [ProgressBar(0, 15, r: 0, g: 0.2f, b: 0.75f)]
        public float ReloadTime;
        
        [ProgressBar(0, 15, r: 0, g: 0.2f, b: 0.75f)]
        public float FullReloadTime;
        
        [ProgressBar(0, 100, r: 0, g: 0.2f, b: 0.75f)]
        public float Range;
        
        [ProgressBar(0, 1000, r: 0, g: 0.2f, b: 0.75f)]
        public float FireRate;
        
        [ProgressBar(0, 1, r: 0.4f, g: 0.2f, b: 0.75f)]
        public float Accuracy;
        
        [ProgressBar(0, 1, r: 0.4f, g: 0.2f, b: 0.75f)]
        public float AimAccuracy;
        
        [ProgressBar(0, 100, r: 0.4f, g: 0.2f, b: 0.75f)]
        public float AccuracyDistance;

        public AmmoType AmmoType;
    }
}