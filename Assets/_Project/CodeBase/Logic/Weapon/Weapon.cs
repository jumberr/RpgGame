using System;
using UnityEngine;

namespace _Project.CodeBase.Logic.Weapon
{
    [Serializable]
    public class Weapon
    {
        // Recoil Settings
        public Vector3 Recoil;
        public Vector3 AimRecoil;
        public float Snappiness;
        public float ReturnSpeed;
        
        public bool IsAutomatic;
        public float Damage;
        public float ReloadTime;
        public float Range;
        public float FireRate;
        
        public float Accuracy;
        public float AimAccuracy;
        public float AccuracyDistance;
    }
}